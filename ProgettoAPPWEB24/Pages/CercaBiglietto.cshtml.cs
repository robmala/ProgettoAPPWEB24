using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    public class CercaBigliettoModel : PageModel
    {
        private readonly IBigliettiRepository _bigliettiRepository;
        private readonly ICostiRepository _costiRepository;
        private readonly IParkingRepository _parkingRepository;

        [BindProperty]
        required public string Targa { get; set; }
        public double Totale { get; set; }
        public TimeSpan Durata { get; set; }
        public Biglietto Biglietto { get; set; } = default!;
        //public Parcheggio? Parcheggio { get; set; } = default!;


        public CercaBigliettoModel(IBigliettiRepository bigliettiRepository, ICostiRepository costiRepository, IParkingRepository parkingRepository)
        {
            _bigliettiRepository = bigliettiRepository;
            _costiRepository = costiRepository;
            _parkingRepository = parkingRepository;
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
                return NotFound("Auto non trovata");
            }

            //Parcheggio = await _parkingRepository.Get(id);
            //if (Parcheggio == null)
            //{
            //    return NotFound("Parcheggio non trovato.");
            //}

            Biglietto = await _bigliettiRepository.Get(Targa, id/*Parcheggio.Id*/);
            if (Biglietto == null)
            {
                return NotFound("Biglietto non trovato");
            }

            Durata = DateTime.Now - Biglietto.Ingresso;
            var costi = await _costiRepository.GetCosto(id/*Parcheggio.Id*/);

            foreach (var c in costi)
            {
                Totale = Biglietto.Ricarica ? ((c.Ricarica + c.Sosta) * (double)Durata.TotalHours) : (c.Sosta * (double)Durata.TotalHours);
            }

            Totale = Math.Round(Totale);

            return Page();
        }

        public async Task<IActionResult> OnPostPaga(Biglietto biglietto, int id)
        {
            var b = await _bigliettiRepository.Get(biglietto.Targa, id);

            if (b == null)
            {
                return NotFound("Biglietto non trovato.");
            }

            return RedirectToPage("Pagamento", new { durata = Durata, biglietto = Biglietto, tariffa = Totale });
        }
    }
}
