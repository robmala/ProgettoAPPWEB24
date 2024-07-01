using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    public class PagamentoModel : PageModel
    {
        private readonly IBigliettiRepository _bigliettiRepository;
        private readonly ICostiRepository _costiRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IAutoRepository _autoRepository;

        private readonly SignInManager<ProgettoAPPWEB24User> _signinManager;
        public Parcheggio? Parcheggio { get; set; }

        [BindProperty]
        required public string Targa { get; set; }
        public double Totale { get; set; }
        public TimeSpan Durata { get; set; }
        public Biglietto? Biglietto { get; set; }
        public string ParkId { get; set; }
        public Pagamento? Pagamento { get; set; }

        public PagamentoModel(IBigliettiRepository bigliettiRepository, ICostiRepository costiRepository, IPagamentoRepository pagamentoRepository, IHttpContextAccessor httpContextAccessor, SignInManager<ProgettoAPPWEB24User> signInManager, IParkingRepository parkingRepository, IAutoRepository autoRepository)
        {
            _bigliettiRepository = bigliettiRepository;
            _costiRepository = costiRepository;
            _pagamentoRepository = pagamentoRepository;
            _signinManager = signInManager;

            var session = httpContextAccessor.HttpContext?.Session ?? throw new NullReferenceException("Missing Session");
            var key = nameof(ParkId);
            ParkId = session.GetString(key) ?? Guid.NewGuid().ToString();
            session.SetString(key, ParkId);
            _parkingRepository = parkingRepository;
            _autoRepository = autoRepository;
        }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPostCerca(int id)
        {
            if (id == 0)
            {
                return NotFound("Parcheggio non trovato.");
            }

            if (string.IsNullOrEmpty(Targa))
            {
                return Page();
            }

            Parcheggio = await _parkingRepository.Get(id);
            if (Parcheggio == null)
            {
                return NotFound("Parcheggio non trovato.");
            }

            Biglietto = await _bigliettiRepository.Get(Targa);
            if (Biglietto == null)
            {
                return Page();
            }

            Durata = DateTime.Now - Biglietto.Ingresso;
            var costi = await _costiRepository.GetCosto(Parcheggio.Id);
            
            foreach (var c in costi)
            {
                Totale = Biglietto.Ricarica ? (double)((c.Ricarica + c.Sosta) * (decimal)Durata.TotalHours) : (double)(c.Sosta * (decimal)Durata.TotalHours);
            }

            Totale = Math.Round(Totale);

            return Page();
        }

        public async Task<IActionResult> OnPostPaga()
        {

            var user = await _signinManager.UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Utente non trovato.");
            }

            Pagamento = new Pagamento
            {
                Utente = user.Id,
                Biglietto = Biglietto!,
                Costo = Totale,
                Uscita = DateTime.Now
            };

            await _pagamentoRepository.AddPagamento(Pagamento!);

            await _parkingRepository.LiberaPosto(Biglietto!.LottoId);
            await _autoRepository.DeleteAuto(Biglietto.Targa);
            await _bigliettiRepository.Delete(Biglietto.Id);

            return RedirectToPage("_PagamentoSuccess");
        }
    }
}
