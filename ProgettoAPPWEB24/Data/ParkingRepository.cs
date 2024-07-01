using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Data
{
    public class ParkingRepository(DataContext context) : DataContextRepository<Parcheggio>(context), IParkingRepository
    {
        public async Task<IEnumerable<Parcheggio>> GetAllParcheggi()
        {
            var parcheggi = await GetAll().ToListAsync();
            return parcheggi.OrderBy(p => p.Nome);
        }

        public async Task<int> GetPostiDisponibili(int id)
        {
            var lotti = await context.Lotti.Where(l => l.ParkId.Equals(id.ToString())).ToListAsync();
            var disponibili = (
                from l in lotti
                where l.IsAvailable
                select l.Id
            ).Count();
            return disponibili;
        }

        public async Task<int> RiservaPosto(int id)
        {
            if (await GetPostiDisponibili(id) > 0)
            {
                var lotti = await context.Lotti.Where(l => l.ParkId == id.ToString()).ToListAsync();
                var lotto = lotti.First(l => l.IsAvailable);
                lotto.IsAvailable = false;
                await context.SaveChangesAsync();
                return lotto.Id;
            }
            else return -1;
        }

        public async Task LiberaPosto(int id)
        {
            var lotto = await context.Lotti.FindAsync(id);
            lotto!.IsAvailable = true;
            await context.SaveChangesAsync();
        }
    }
}
