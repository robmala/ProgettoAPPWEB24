using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    public class PagamentoModel : PageModel
    {
        private readonly IBigliettiRepository _bigliettiRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IAutoRepository _autoRepository;

        private readonly SignInManager<ProgettoAPPWEB24User> _signinManager;
        public Pagamento Pagamento { get; set; } = default!;
        public TimeSpan Durata { get; set; }

        public PagamentoModel(IBigliettiRepository bigliettiRepository, IPagamentoRepository pagamentoRepository, SignInManager<ProgettoAPPWEB24User> signInManager, IParkingRepository parkingRepository, IAutoRepository autoRepository)
        {
            _bigliettiRepository = bigliettiRepository;
            _pagamentoRepository = pagamentoRepository;
            _parkingRepository = parkingRepository;
            _autoRepository = autoRepository;
            _signinManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(TimeSpan durata, Biglietto biglietto, double tariffa)
        {

            if (biglietto == null)
            {
                return RedirectToPage("Dati mancanti.");
            }

            var user = await _signinManager.UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Utente non trovato.");
            }

            Durata = durata;

            Pagamento = new Pagamento
            {
                Utente = user.Email!,
                IdParcheggio = biglietto.IdParcheggio,
                Ingresso = biglietto.Ingresso,
                Uscita = DateTime.Now,
                Costo = tariffa,
                Ricarica = biglietto.Ricarica
            };

            await _pagamentoRepository.AddPagamento(Pagamento!);

            await _parkingRepository.LiberaPosto(biglietto!.LottoId);
            await _autoRepository.DeleteAuto(biglietto.Targa);
            await _bigliettiRepository.Delete(biglietto.Id);

            return RedirectToPage("_PagamentoSuccess");
        }
    }
}
