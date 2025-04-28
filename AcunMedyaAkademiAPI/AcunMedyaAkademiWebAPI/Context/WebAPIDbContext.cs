using AcunMedyaAkademiWebAPI.Entites;
using Microsoft.EntityFrameworkCore;

namespace AcunMedyaAkademiWebAPI.Context
{
    public class WebAPIDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-493DFJA\\SQLEXPRESS; initial catalog=DbAcunMedyaWebApi; integrated Security=true; TrustServerCertificate=True");
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
