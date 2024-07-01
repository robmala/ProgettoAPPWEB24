using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IParkingRepository : IRepository<Parcheggio>
    {
        Task<IEnumerable<Parcheggio>> GetAllParcheggi();
        Task<int> GetPostiDisponibili(int id);
        Task<int> RiservaPosto(int id);
        Task LiberaPosto(int id);
    }
}
