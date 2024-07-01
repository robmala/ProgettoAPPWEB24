using ProgettoAPPWEB24.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ProgettoAPPWEB24.Models
{
	public class Lotto : IHasId
	{
		public int Id { get; set; }
		public bool IsAvailable { get; set; }
		required public string ParkId { get; set; }
	}
}
