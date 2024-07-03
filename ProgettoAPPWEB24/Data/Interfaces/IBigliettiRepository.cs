namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IBigliettiRepository : IRepository<Biglietto>
    {
        public Task<Biglietto> Get(string targa, int id);
        public Task AddBiglietto(Biglietto biglietto);
    }
}
