using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using ProgettoAPPWEB24.Data.Interfaces;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public Biglietto Bigl { get; set; }


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

            var b = Bigl = await _bigliettiRepository.Get(Targa, id);
            if (b == null)
            {
                return NotFound("Biglietto non trovato");
            }

            Durata = DateTime.Now - b.Ingresso;
            var costi = await _costiRepository.GetCosto(id);

            foreach (var c in costi)
            {
                Totale = b.Ricarica ? ((c.Ricarica + c.Sosta) * (double)Durata.TotalHours) : (c.Sosta * (double)Durata.TotalHours);
            }

            Totale = Math.Round(Totale);

            return Page();
        }
    }
}
