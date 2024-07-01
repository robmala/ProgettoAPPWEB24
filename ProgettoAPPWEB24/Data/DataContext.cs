using ProgettoAPPWEB24.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace ProgettoAPPWEB24.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ProgettoAPPWEB24User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Parcheggio> Parcheggi => Set<Parcheggio>();
    public DbSet<Lotto> Lotti => Set<Lotto>();
	public DbSet<Costo> Costi => Set<Costo>();
    public DbSet<Auto> Auto => Set<Auto>();
    public DbSet<Pagamento> Pagamenti => Set<Pagamento>();
    public DbSet<Biglietto> Biglietti => Set<Biglietto>();

}