namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IBigliettiRepository : IRepository<Biglietto>
    {
        public Task<Biglietto> Get(string targa);
        public Task AddBiglietto(Biglietto biglietto);
    }
}
