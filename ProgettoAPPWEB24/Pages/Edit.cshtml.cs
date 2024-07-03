using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICostiRepository _costiRepository;

        public IEnumerable<Costo> CostoIniziale { get; set; } = [];

        public Costo? CostoNuovo { get; set; }

        [BindProperty]
        public double Sosta { get; set; }
        [BindProperty]
        public double Ricarica { get; set; }

        public EditModel(ICostiRepository costiRepository)
        {
            _costiRepository = costiRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            CostoIniziale = await _costiRepository.GetCosto(id);
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CostoNuovo = new Costo
            {
                IdParcheggio = id,
                Sosta = Sosta,
                Ricarica = Ricarica
            };
            await _costiRepository.Update(CostoNuovo!);
            return RedirectToPage("Index");
        }
    }
}
