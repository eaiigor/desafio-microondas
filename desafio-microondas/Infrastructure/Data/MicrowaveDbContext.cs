using desafio_microondas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafio_microondas.Infrastructure.Data
{
    public class MicrowaveDbContext : DbContext
    {
        public DbSet<Microwave> Microwave { get; set; }
        public DbSet<HeatingProgram> HeatingProgram { get; set; }

        public MicrowaveDbContext(DbContextOptions<MicrowaveDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Microwave>().HasKey(m => m.Id);
            modelBuilder.Entity<HeatingProgram>()
                .HasOne<Microwave>(m => m.Microwave)
                .WithMany(m => m.HeatingPrograms)
                .HasForeignKey(m => m.MicrowaveId);

            modelBuilder.Entity<HeatingProgram>().HasKey(h => h.Id);
        }
    }
}