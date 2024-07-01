using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
    public class Auto : IHasId
    {
        public int Id { get; set; }
        required public string Targa { get; set; }
        public decimal CapienzaBatteria { get; set; }
        public int LivelloBatteria { get; set; }
        public bool IsRecharging { get; set; } = false;
    }
}
