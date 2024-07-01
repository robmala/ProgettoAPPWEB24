using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoAPPWEB24.Models;
using ProgettoAPPWEB24.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace ProgettoAPPWEB24.Data
{
    public class PagamentoRepository : DataContextRepository<Pagamento>, IPagamentoRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<PagamentoRepository> _logger;
        public string LottoId { get; private set; }

        public PagamentoRepository(DataContext dataContext, IHttpContextAccessor httpContextAccessor, ILogger<PagamentoRepository> logger) : base(dataContext)
        {

            _dataContext = dataContext;
            var session = httpContextAccessor.HttpContext?.Session ?? throw new NullReferenceException("Missing Session");

            var key = nameof(LottoId);
            var lottoId = session.GetInt32(key)?.ToString() ?? Guid.NewGuid().ToString();
            session.SetString(key, lottoId);

            LottoId = lottoId;
            _logger = logger;
        }

        public async Task<decimal> GetTotaleAsync()
        {
            var lotto = await _dataContext.Lotti.FindAsync(LottoId);

            var uscita = DateTime.Now.Hour;
            return lotto!.Id * uscita;
        }

        public async Task<IEnumerable<Pagamento>> GetStorico()
        {
            var storico = await _dataContext.Pagamenti.ToListAsync();
            return storico;
        }

        public async Task AddPagamento(Pagamento pagamento)
        {
            _dataContext.Pagamenti.Add(pagamento);
            _logger.LogInformation("Pagamento effettuato");
            await _dataContext.SaveChangesAsync();
        }
    }
}
