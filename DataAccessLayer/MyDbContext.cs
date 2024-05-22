using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString = @"Server=.\SQLExpress;Database=DBtap;Trusted_Connection=True;TrustServerCertificate=true";

        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Furnizor> Furnizori { get; set; }
        public DbSet<Departament> Departamente { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<ProdusComanda> ProduseComenzi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurare relații pentru entitatea Produs
            builder.Entity<Produs>()
                .HasOne(p => p.Furnizor) // Un produs are un singur furnizor
                .WithMany(f => f.Produse) // Un furnizor poate furniza mai multe produse
                .HasForeignKey(p => p.FurnizorId); // Cheia externă în Produs este FurnizorId

            builder.Entity<Produs>()
                .HasOne(p => p.Departament) // Un produs aparține unui singur departament
                .WithMany(d => d.Produse) // Un departament poate avea mai multe produse
                .HasForeignKey(p => p.DepartamentId); // Cheia externă în Produs este DepartamentId

            // Configurare relații pentru entitatea ProdusComanda
            builder.Entity<ProdusComanda>()
                .HasKey(pc => new { pc.ProdusId, pc.ComandaId }); // Cheia primară compusă

            builder.Entity<ProdusComanda>()
                .HasOne(pc => pc.Produs) // Un ProdusComanda are un singur produs
                .WithMany(p => p.ProdusComenzi) // Un produs poate fi parte a mai multor comenzi
                .HasForeignKey(pc => pc.ProdusId); // Cheia externă în ProdusComanda este ProdusId

            builder.Entity<ProdusComanda>()
                .HasOne(pc => pc.Comanda) // Un ProdusComanda are o singură comandă
                .WithMany(c => c.ProdusComenzi) // O comandă poate conține mai multe produse
                .HasForeignKey(pc => pc.ComandaId); // Cheia externă în ProdusComanda este ComandaId

            

        }

        public void SeedData()
        {

            if (!Produse.Any() &&
                !Furnizori.Any() &&
                !Departamente.Any() &&
                !Comenzi.Any() && !ProduseComenzi.Any())
            {
                // Seed data pentru Furnizori
                this.Furnizori.AddRange(
                    new Furnizor("Furnizor1", "contact1@example.com", "Adresa1"),
                    new Furnizor("Furnizor2", "contact2@example.com", "Adresa2")
                );

                // Seed data pentru Departamente
                this.Departamente.AddRange(
                    new Departament("Departament1"),
                    new Departament("Departament2")
                );

                // Salvăm modificările înainte de a accesa ID-urile
                this.SaveChanges();

                // Obținem ID-urile furnizorilor și departamentelor
                var furnizor1 = this.Furnizori.First(f => f.Nume == "Furnizor1");
                var furnizor2 = this.Furnizori.First(f => f.Nume == "Furnizor2");
                var departament1 = this.Departamente.First(d => d.Nume == "Departament1");
                var departament2 = this.Departamente.First(d => d.Nume == "Departament2");

                // Seed data pentru Produse
                this.Produse.AddRange(
                    new Produs("Produs1", "Descriere produs 1", 10, DateTime.Now.AddDays(30), furnizor1.Id, departament1.Id),
                    new Produs("Produs2", "Descriere produs 2", 20, DateTime.Now.AddDays(60), furnizor2.Id, departament2.Id)
                );

                // Seed data pentru Comenzi
                this.Comenzi.AddRange(
                    new Comanda("NumeComanda1", DateTime.Now),
                    new Comanda("NumeComanda2", DateTime.Now.AddDays(-1))
                );

                // Salvăm din nou modificările pentru a adăuga toate datele de seed
                this.SaveChanges();

                // Obținem ID-urile pentru produsele și comenzile adăugate
                var produs1Id = this.Produse.First(p => p.Nume == "Produs1").Id;
                var produs2Id = this.Produse.First(p => p.Nume == "Produs2").Id;
                var comanda1Id = this.Comenzi.First().Id;
                var comanda2Id = this.Comenzi.Skip(1).First().Id;

                // Seed data pentru ProduseComenzi
                this.ProduseComenzi.AddRange(
                    new ProdusComanda ( produs1Id, comanda1Id, 8),
                    new ProdusComanda ( produs2Id, comanda2Id, 15 )
                );

                // Salvăm modificările pentru a adăuga datele de seed pentru ProduseComenzi
                this.SaveChanges();
            }
        }


    }
}
