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
        public Biglietto Biglietto { get; set; } = default!;
        //public Parcheggio Parcheggio { get; set; } = default!;

        public SostaModel(IAutoRepository autoRepository, UserManager<ProgettoAPPWEB24User> userManager, IParkingRepository parkingRepository, IBigliettiRepository bigliettiRepository)
        {
            _autoRepository = autoRepository;
            _userManager = userManager;
            _parkingRepository = parkingRepository;
            _bigliettiRepository = bigliettiRepository;

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

            var utente = await _userManager.GetUserAsync(User);
            if (utente == null)
            {
                return NotFound("Utente non trovato.");
            }

            var lotto = await _parkingRepository.RiservaPosto(id);
            if (lotto == -1)
            {
                return BadRequest("Parcheggio pieno.");
            }

            var biglietti = await _bigliettiRepository.GetAll().ToListAsync();

            if (biglietti.Any(b => b.Targa == InputModel.Targa))
            {
                return BadRequest("Biglietto già presente.");
            }
            else
            {
                await _autoRepository.AddAuto(InputModel);
                Biglietto = new Biglietto
                {
                    Targa = InputModel.Targa,
                    IdParcheggio = id,
                    LottoId = lotto
                };
                await _bigliettiRepository.AddBiglietto(Biglietto);
                return RedirectToPage("_SostaSuccess");
            }

        }
    }
}
