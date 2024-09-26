using ECOMMERCEFAZZY.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCEFAZZY.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Order)
          .WithMany(o => o.OrderItems)
          .HasForeignKey(oi => oi.OrderId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
            .HasOne(oi => oi.Category)
            .WithMany(o => o.Products)
            .HasForeignKey(oi => oi.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Product)
          .WithMany()
          .HasForeignKey(oi => oi.ProductId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
          .HasOne(ci => ci.Cart)
          .WithMany(c => c.CartItems)
          .HasForeignKey(ci => ci.CartId)
          .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
