using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Data.Interfaces;
using ProgettoAPPWEB24.Models;
using System.ComponentModel;

namespace ProgettoAPPWEB24.Data
{
    public class AutoRepository : DataContextRepository<Auto>, IAutoRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<ProgettoAPPWEB24User> _userManager;

        public AutoRepository(DataContext dataContext, UserManager<ProgettoAPPWEB24User> userManager) : base(dataContext)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task AddAuto(Auto auto)
        {
            await _dataContext.Auto.AddAsync(auto);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAuto(string targa)
        {
            var auto = await _dataContext.Auto.FirstOrDefaultAsync(a => a.Targa == targa);
            if (auto != null)
            {
                _dataContext.Auto.Remove(auto);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Auto>> GetAllAuto()
        {
            return await _dataContext.Auto.ToListAsync();
        }
    }
}
