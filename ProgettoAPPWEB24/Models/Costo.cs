using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
    public class Costo : IHasId
    {
        public int Id { get; set; }
        public int IdParcheggio { get; set; }
        public double Ricarica { get; set; }
        public double Sosta { get; set; }
    }
}
