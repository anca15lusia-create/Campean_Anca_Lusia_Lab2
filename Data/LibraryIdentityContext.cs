using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options) : IdentityDbContext<Campean_Anca_Lusia_Lab2.Data.ApplicationUser>(options)
{
}