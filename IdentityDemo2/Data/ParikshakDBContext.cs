using IdentityDemo2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityDemo2.DTOs;

namespace IdentityDemo2.Data
{
    public class ParikshakDBContext : IdentityDbContext<ApplicationUser>
    {
        public ParikshakDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<IdentityDemo2.DTOs.RoleStore>? RoleStore { get; set; }
        public DbSet<Contest> Contestes { get; set; }
        public DbSet<Problem> Problemes { get; set; }


    }
}
