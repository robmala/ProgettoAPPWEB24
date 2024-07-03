using ProgettoAPPWEB24.Data;
using ProgettoAPPWEB24.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoAPPWEB24.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using ProgettoAPPWEB24.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("(default)") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDefaultIdentity<ProgettoAPPWEB24User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
	var configuration = builder.Configuration;
	googleOptions.ClientId = configuration["Authentication:Google:ClientId"]!;
	googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

AddRepositories();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await AddAdmin();

app.Run();

void AddRepositories()
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    services.AddScoped<IParkingRepository, ParkingRepository>();
    services.AddScoped<IPagamentoRepository, PagamentoRepository>();
    services.AddScoped<IAutoRepository, AutoRepository>();
    services.AddScoped<ICostiRepository, CostiRepository>();
    services.AddScoped<IBigliettiRepository, BigliettiRepository>();
}

async Task AddAdmin()
{
    var services = app!.Services!.CreateScope().ServiceProvider;

    var configuration = new User();
    app.Configuration.Bind("Admin", configuration);

    var usersManager = services.GetRequiredService<UserManager<ProgettoAPPWEB24User>>();

    var user = new ProgettoAPPWEB24User() { UserName = configuration.UserName!, Role = "Admin"};
    if((await usersManager.CreateAsync(user, configuration.Password!)).Succeeded)
    {
        await usersManager.AddClaimAsync(user, new(nameof(Role), Role.Admin));
    }
}

record User
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
 }