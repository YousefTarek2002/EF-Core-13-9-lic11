using System;
using System.Collections.Generic;
using EF_Core_13_9.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_13_9.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=\"DESKTOP-1G3CFH9\";Initial Catalog=BikeStores518;Integrated Security=True;Connect " +
            "Timeout=30;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E27DFDF2E01");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B421EF0A56");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85408C94BE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229CE8DC127");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__orders__customer__47DBAE45");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__staff_id__49C3F6B7");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("FK__orders__store_id__48CFD27E");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId }).HasName("PK__order_it__837942D453D25B33");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__order_ite__order__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems).HasConstraintName("FK__order_ite__produ__4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5D8042290");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasConstraintName("FK__products__brand___3C69FB99");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK__products__catego__3B75D760");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staffs__1963DD9CBCD488B0");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager).HasConstraintName("FK__staffs__manager___44FF419A");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff).HasConstraintName("FK__staffs__store_id__440B1D61");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK__stocks__E68284D3974427D9");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks).HasConstraintName("FK__stocks__product___52593CB8");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks).HasConstraintName("FK__stocks__store_id__5165187F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__stores__A2F2A30C3BEA2252");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
