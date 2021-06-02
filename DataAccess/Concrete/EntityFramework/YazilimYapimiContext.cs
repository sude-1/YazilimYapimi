using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context veri tabanı tabloları ile proje classlarını bağlamak
    public class YazilimYapimiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=yazilimyapimi;Trusted_Connection=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AddMoney> AddMoney { get; set; }
        public DbSet<AddProduct> AddProducts { get; set; }
        public DbSet<UserWallet> UserWallet { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
