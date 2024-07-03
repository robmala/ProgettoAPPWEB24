using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
    public class Biglietto : IHasId
    {
        public int Id { get; set; }
        required public string Targa { get; set; }
        required public int LottoId { get; set; }
        required public int IdParcheggio { get; set; }
        public DateTime Ingresso { get; set; } = DateTime.Now;
        public bool Ricarica { get; set; } = false;

    }
}
