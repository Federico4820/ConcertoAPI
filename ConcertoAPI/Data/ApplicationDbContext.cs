using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ConcertoAPI.Models;
using ConcertoAPI.Models.Auth;

namespace ConcertoAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Artista)
                .WithMany(a => a.Eventi)
                .HasForeignKey(e => e.ArtistaId);

            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.Evento)
                .WithMany(e => e.Biglietti)
                .HasForeignKey(b => b.EventoId);

            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.Utente)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);
        }
    }
}
