using API_ECommerce.Context.Mappings;
using API_ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ECommerce.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
        }
        public DbSet<ClienteModel> cliente { get; set; }

    }

   
}
