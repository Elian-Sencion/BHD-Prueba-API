using BHD_Prueba_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BHD_Prueba_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Phone> Phones => Set<Phone>();
    }
}

