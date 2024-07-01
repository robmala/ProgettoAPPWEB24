using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
    public class Parcheggio : IHasId
    {
        public int Id { get; set; }
        required public string Nome { get; set; }
        required public string Indirizzo { get; set; }
        required public string Citta { get; set; }
        required public string Provincia { get; set; }
        required public string CAP { get; set; }
        required public string Orari { get; set; }
    }
}
