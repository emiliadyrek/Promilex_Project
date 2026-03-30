using Microsoft.EntityFrameworkCore;

namespace Promiex.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Producent> Producenci { get; set; }
        public DbSet<Dostawca> Dostawcy { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<Recenzja> Recenzje { get; set; }
    }
}