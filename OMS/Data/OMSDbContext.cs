using Microsoft.EntityFrameworkCore;
using OMS.Models;
using System.Collections.Generic;

namespace OMS.Data
{
    public class OMSDbContext : DbContext
    {
        public OMSDbContext(DbContextOptions<OMSDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure SerialNumbers to be stored in JSON form
            modelBuilder.Entity<OrderProduct>()
                .Property(op => op.SerialNumbers)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, new System.Text.Json.JsonSerializerOptions()));
        }
    }
}
