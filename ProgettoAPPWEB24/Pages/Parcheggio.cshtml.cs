using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Pages
{
    [Authorize(Roles = Role.Admin)]
    [AllowAnonymous]
    public class ParcheggioModel : PageModel
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly ICostiRepository _costiRepository;
        public IEnumerable<Costo>? Costo { get; set; }
        public Parcheggio? Parcheggio { get; set; }
        public int Disponibili { get; set; }

        public ParcheggioModel(IParkingRepository parkingRepository, ICostiRepository costiRepository)
        {
            _parkingRepository = parkingRepository;
            _costiRepository = costiRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Parcheggio = await _parkingRepository.Get(id);
            Disponibili = await _parkingRepository.GetPostiDisponibili(Parcheggio!.Id);
            Costo = await _costiRepository.GetCosto(id);

            if (Parcheggio == null)
            {
                return NotFound("Parcheggio non trovato.");
            }
            else
            {
                return Page();
            }
        }
    }
}
