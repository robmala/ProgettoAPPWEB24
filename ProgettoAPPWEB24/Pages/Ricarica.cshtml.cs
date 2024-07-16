using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Data;
using ProgettoAPPWEB24.Data.Interfaces;
using ProgettoAPPWEB24.Models;

namespace ProgettoAPPWEB24.Pages
{
    public class RicaricaModel : PageModel
    {
        private readonly IBigliettiRepository _bigliettiRepository;
        private readonly IAutoRepository _autoRepository;
        private readonly UserManager<ProgettoAPPWEB24User> _userManager;
        private readonly IParkingRepository _parkingRepository;

        [BindProperty]
        required public Auto InputModel { get; set; }
        [BindProperty]
        public int Livello { get; set; }
        public Biglietto Biglietto { get; set; } = default!;

        private static readonly Random rnd = new Random();
        public int Count = 0;

        public RicaricaModel(IBigliettiRepository bigliettiRepository, IAutoRepository autoRepository, UserManager<ProgettoAPPWEB24User> userManager, IParkingRepository parkingRepository)
        {
            _autoRepository = autoRepository;
            _userManager = userManager;
            _parkingRepository = parkingRepository;
            _bigliettiRepository = bigliettiRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var listaAuto = await _autoRepository.GetAllAuto();
            foreach (var auto in listaAuto)
            {
                if (auto.IsRecharging) Count++;
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
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
                InputModel.CapienzaBatteria = rnd.Next(50, 71); // (Simulatore) Setto la capienza della batteria dell'auto ad un valore randomico compreso tra 50 e 70 kWh
                await _autoRepository.AddAuto(InputModel);

                Biglietto = new Biglietto
                {
                    Targa = InputModel.Targa,
                    IdParcheggio = id,
                    LottoId = lotto,
                    Ricarica = true
                };
                await _bigliettiRepository.AddBiglietto(Biglietto);
                return RedirectToPage("_SostaSuccess");
            }
        }
    }
}
