using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
	public class Costo : IHasId
	{
		public int Id { get; set; }
		public int IdParcheggio { get; set; }
		public decimal Ricarica { get; set; }
		public decimal Sosta { get; set; }
	}
}
