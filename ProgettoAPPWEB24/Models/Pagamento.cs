﻿using NuGet.Packaging.Signing;
using ProgettoAPPWEB24.Data.Interfaces;

namespace ProgettoAPPWEB24.Models
{
    public class Pagamento : IHasId
    {
        public int Id { get; set; }
        required public string Utente { get; set; }
        required public Biglietto Biglietto { get; set; }
        public DateTime Uscita { get; set; }
        public double Costo { get; set; }
    }
}