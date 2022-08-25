using Dominio.Usuarios;
using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class MuiiContext : IdentityDbContext<Usuario>
    {
        public MuiiContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            modelBuilder.Entity<RestrincionAnimal>().HasKey(ci => new { ci.AnimalId, ci.AnimalNoConviveId });
            modelBuilder.Entity<CorralAnimal>().HasKey(ci => new { ci.AnimalId, ci.CorralId });
            modelBuilder.Entity<RestrincionAnimal>()
                    .HasOne(p => p.Animal).WithMany(a => a.RestrincionLink);


        }

        public DbSet<Corral> Corral { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<CorralAnimal> CorralAnimal { get; set; }
        public DbSet<RestrincionAnimal> RestrincionAnimal { get; set; }
    }
}