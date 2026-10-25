using Microsoft.EntityFrameworkCore;

public class Campean_Anca_Lusia_Lab2Context(
    DbContextOptions<Campean_Anca_Lusia_Lab2Context> options)
    : DbContext(options)
{
    public DbSet<Campean_Anca_Lusia_Lab2.Models.Book> Book
    { get; set; } = default!;

    public DbSet<Campean_Anca_Lusia_Lab2.Models.Publisher> Publisher
    { get; set; } = default!;

    public DbSet<Campean_Anca_Lusia_Lab2.Models.Authors> Authors
    { get; set; } = default!;

    public DbSet<Campean_Anca_Lusia_Lab2.Models.Category> Category
    { get; set; } = default!;
}