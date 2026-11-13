using Microsoft.EntityFrameworkCore;
using Campean_Anca_Lusia_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Campean_Anca_Lusia_Lab2Context")
    ?? throw new InvalidOperationException("Connection string 'Campean_Anca_Lusia_Lab2Context' not found.");

var identityConnectionString = builder.Configuration.GetConnectionString("LibraryIdentityContext")
    ?? throw new InvalidOperationException("Connection string 'LibraryIdentityContext' not found.");

builder.Services.AddDbContext<Campean_Anca_Lusia_Lab2Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publisher", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/CategoryPages", "AdminPolicy");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();