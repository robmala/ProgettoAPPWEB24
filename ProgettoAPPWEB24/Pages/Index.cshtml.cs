using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Data.Interfaces;
using ProgettoAPPWEB24.Models;

namespace ProgettoAPPWEB24.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IParkingRepository _parkingRepository;
        public IEnumerable<Parcheggio> Parcheggi { get; set; }

        public IndexModel(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
            Parcheggi = Enumerable.Empty<Parcheggio>();
        }

        public async Task OnGet()
        {
            Parcheggi = await _parkingRepository.GetAllParcheggi();
        }
    }
}
