using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TPServiceScolarite.Models
{
    public class ScolariteDbEntities : IdentityDbContext
    {
        public ScolariteDbEntities(DbContextOptions<ScolariteDbEntities> opts) : base(opts)
        { 
        }

        public DbSet<Parcour> Parcours { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
