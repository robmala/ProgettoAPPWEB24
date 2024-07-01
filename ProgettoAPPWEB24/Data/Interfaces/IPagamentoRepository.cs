using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<decimal> GetTotaleAsync();
        Task<IEnumerable<Pagamento>> GetStorico();
        Task AddPagamento(Pagamento pagamento);
    }
}
