namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface ICostiRepository : IRepository<Costo>
    {
        public Task<IEnumerable<Costo>> GetCosto(int id);
    }
}
