using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreDL.Entities
{
    public partial class p0storeContext : DbContext
    {
        public p0storeContext()
        {
        }

        public p0storeContext(DbContextOptions<p0storeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Customer__536C85E555A7C75A");

                entity.ToTable("Customer");

                entity.Property(e => e.Username).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(80);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(38, 36)");

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Items__LocationI__6AEFE058");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Items__OrderId__6BE40491");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationAddress).HasMaxLength(100);

                entity.Property(e => e.LocationName).HasMaxLength(80);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Cusername)
                    .HasMaxLength(80)
                    .HasColumnName("CUsername");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(38, 36)")
                    .HasColumnName("total");

                entity.HasOne(d => d.CusernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Cusername)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Orders__CUsernam__671F4F74");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
