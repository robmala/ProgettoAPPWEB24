using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    public class SostaModel : PageModel
    {
        private readonly IAutoRepository _autoRepository;
        private readonly UserManager<ProgettoAPPWEB24User> _userManager;
        private readonly IParkingRepository _parkingRepository;
        private readonly IBigliettiRepository _bigliettiRepository;

        [BindProperty]
        required public Auto InputModel { get; set; }
        public Biglietto? Biglietto { get; set; }
        public string ParkId { get; set; }
        public Parcheggio? Parcheggio { get; set; }

        public SostaModel(IAutoRepository autoRepository, UserManager<ProgettoAPPWEB24User> userManager, IParkingRepository parkingRepository, IHttpContextAccessor httpContextAccessor, IBigliettiRepository bigliettiRepository)
        {
            _autoRepository = autoRepository;
            _userManager = userManager;
            _parkingRepository = parkingRepository;
            _bigliettiRepository = bigliettiRepository;

            var session = httpContextAccessor.HttpContext?.Session ?? throw new NullReferenceException("Missing Session");
            var key = nameof(ParkId);
            ParkId = session.GetString(key) ?? Guid.NewGuid().ToString();
            session.SetString(key, ParkId);
        }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(id == 0)
            {
                return NotFound("Parcheggio non trovato.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Parcheggio = await _parkingRepository.Get(id);
            if (Parcheggio == null)
            {
                return NotFound("Parcheggio non trovato.");
            }

            var utente = await _userManager.GetUserAsync(User);
            if (utente == null)
            {
                return NotFound("Utente non trovato.");
            }

            var lotto = await _parkingRepository.RiservaPosto(Parcheggio.Id);
            if (lotto == -1)
            {
                return BadRequest("Parcheggio pieno.");
            }

            var auto = await _autoRepository.GetAllAuto();
            var biglietti = await _bigliettiRepository.GetAll().ToListAsync();

            if (auto.Any(a => a.Targa == InputModel.Targa))
            {
                Biglietto = new Biglietto
                {
                    Targa = InputModel.Targa,
                    ParkId = ParkId,
                    LottoId = lotto
                };
                await _bigliettiRepository.AddBiglietto(Biglietto);
                return RedirectToPage("_SostaSuccess");
            }
            else
            {
                await _autoRepository.AddAuto(InputModel);
                Biglietto = new Biglietto
                {
                    Targa = InputModel.Targa,
                    ParkId = ParkId,
                    LottoId = lotto
                };
                await _bigliettiRepository.AddBiglietto(Biglietto);
                return RedirectToPage("_SostaSuccess");
            }

        }
    }
}
