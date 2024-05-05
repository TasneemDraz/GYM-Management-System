using GYM_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GYM_Management_System.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Members> Members { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Trainers> Trainers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Classes>()
                .HasOne(c => c.Trainers)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TrainersId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of Trainers if Classes are associated

            modelBuilder.Entity<Members>()
                .HasOne(m => m.Trainers)
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.TrainersId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of Trainers if Members are associated


        }

    }
}
