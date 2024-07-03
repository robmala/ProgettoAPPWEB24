using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Data
{
    public class CostiRepository : DataContextRepository<Costo>, ICostiRepository
    {
        readonly DataContext _dataContext;
        public CostiRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Costo>> GetCosto(int id)
        {
            var costi = await _dataContext.Costi.Where(c => c.IdParcheggio == id).ToListAsync();
            return costi;
        }

        public async Task Update(Costo costo)
        {
            var co = await _dataContext.Costi.FirstOrDefaultAsync(c => c.IdParcheggio == costo.IdParcheggio);
            co!.Ricarica = costo.Ricarica;
            co!.Sosta = costo.Sosta;
            _dataContext.Costi.Update(co);
            await _dataContext.SaveChangesAsync();
        }
    }
}
