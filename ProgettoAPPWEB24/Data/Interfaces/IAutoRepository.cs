using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Models;

namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IAutoRepository : IRepository<Auto>
    {
        public Task AddAuto(Auto auto);
        public Task DeleteAuto(string targa);
        public Task<IEnumerable<Auto>> GetAllAuto();
    }

}
