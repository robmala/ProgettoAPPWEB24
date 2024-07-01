using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Data.Interfaces;
using System.Threading.Tasks;
using System;

namespace ProgettoAPPWEB24.Pages
{
    public class StoricoModel : PageModel
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        public IEnumerable<Pagamento>? Storico { get; set; }

        public StoricoModel(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Storico = await _pagamentoRepository.GetStorico();
            return Page();
        }
    }
}
