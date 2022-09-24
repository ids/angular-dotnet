using AngularDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularDotNet.Context
{
    public class AngularDotNetContext : DbContext
    {
        public AngularDotNetContext(DbContextOptions<AngularDotNetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}