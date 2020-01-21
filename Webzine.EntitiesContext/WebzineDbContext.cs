using Microsoft.EntityFrameworkCore;
using Webzine.Entity;

namespace Webzine.EntitiesContext
{
    /// <summary>
    /// DBContext de notre solution
    /// </summary>
    public class WebzineDbContext : DbContext
    {
        public DbSet<Titre> Titres { get; set; }
        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<LienStyle> LienStyles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuration du Provider
            optionsBuilder.UseSqlite(@"Data Source=WebzineDatabase.db");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Webzine;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Création des relations entre entitées (en base)
            // Exemple :

            // Définition de la table (Titre: entité à mapper, "Titres": nom de la table)
            modelBuilder.Entity<Titre>().ToTable("Titres");
            // Définition de la clé primaire (IdTitre)
            modelBuilder.Entity<Titre>().HasKey(p => p.IdTitre);
            // Définition de la relation One to Many (un Titre contient un artiste, et un artiste plusieurs titres) avec la clé étrangère IdArtiste
            modelBuilder.Entity<Titre>().HasOne(p => p.Artiste).WithMany(p => p.Titres).HasForeignKey(p => p.IdArtiste);
            // Définition de la relation Many To One (un Titre contient plusieurs commentaires, qui contient un titre)
            modelBuilder.Entity<Titre>().HasMany(p => p.Commentaires).WithOne(p => p.Titre).HasForeignKey(p => p.IdTitre);

            modelBuilder.Entity<Artiste>().ToTable("Artistes");
            modelBuilder.Entity<Artiste>().HasKey(p => p.IdArtiste);
            modelBuilder.Entity<Artiste>().HasMany(p => p.Titres).WithOne(p => p.Artiste).HasForeignKey(p => p.IdArtiste);
            modelBuilder.Entity<Artiste>().HasOne(p => p.Pays).WithMany(p => p.Artistes).HasForeignKey(p => p.IdPays);


            modelBuilder.Entity<Pays>().ToTable("Pays");
            modelBuilder.Entity<Pays>().HasKey(p => p.IdPays);
            modelBuilder.Entity<Pays>().HasMany(p => p.Artistes).WithOne(p => p.Pays).HasForeignKey(p => p.IdPays);

            modelBuilder.Entity<Style>().ToTable("Styles");
            modelBuilder.Entity<Style>().HasKey(p => p.IdStyle);

            modelBuilder.Entity<Commentaire>().ToTable("Commentaires");
            modelBuilder.Entity<Commentaire>().HasKey(p => p.IdCommentaire);
            modelBuilder.Entity<Commentaire>().HasOne(p => p.Titre).WithMany(p => p.Commentaires).HasForeignKey(p => p.IdTitre);

            modelBuilder.Entity<LienStyle>().ToTable("LiensStyle");
            modelBuilder.Entity<LienStyle>().HasKey(c => new { c.IdStyle, c.IdTitre });
            modelBuilder.Entity<LienStyle>().HasOne(p => p.Style).WithMany(p => p.LienStyle).HasForeignKey(p => p.IdStyle);
            modelBuilder.Entity<LienStyle>().HasOne(p => p.Titre).WithMany(p => p.LienStyle).HasForeignKey(p => p.IdTitre);

        }
    }
}
