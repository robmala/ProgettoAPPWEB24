using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProgettoAPPWEB24.Data
{
    public class BigliettiRepository : DataContextRepository<Biglietto>, IBigliettiRepository
    {
        private readonly DataContext _dataContext;
        public BigliettiRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Biglietto> Get(string targa, int id)
        {
            var biglietto = await _dataContext.Biglietti.FirstOrDefaultAsync(b => b.Targa == targa && b.IdParcheggio == id );
            return biglietto!;
        }

        public async Task AddBiglietto(Biglietto biglietto)
        {
            
            await _dataContext.Biglietti.AddAsync(biglietto);
            await _dataContext.SaveChangesAsync();
            
        }
    }
}
