using Microsoft.EntityFrameworkCore;

namespace activity1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Confirmation>Confirmations{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BSC-PG02KEHR\\SQLEXPRESS;DataBase=ActivityDB;Integrated Security=true;TrustServerCertificate=true");
        }
    }
}
