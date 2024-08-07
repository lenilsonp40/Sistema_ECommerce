﻿using API_ECommerce.Context.Mappings;
using API_ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_ECommerce.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama a implementação base para configurar as entidades de identidade
            base.OnModelCreating(modelBuilder);

            // aqui aplica as concifgurações personalizadas
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());

        }
        public DbSet<ClienteModel> cliente { get; set; }
        public DbSet<ProdutoModel> produto { get; set; }


    }


}