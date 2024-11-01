using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace QuickKartDataAccessLayer.Models
{
    public partial class QuickKartDBContext : DbContext
    {
        public QuickKartDBContext()
        {
        }

        public QuickKartDBContext(DbContextOptions<QuickKartDBContext> options)
            : base(options)
        {
        }
        [DbFunction("ufn_GenerateNewProductId", "dbo")]
        public static string GenerateProductID()
        {
            return null;
        }

        [DbFunction("ufn_GenerateNewCategoryId", "dbo")]
        public static int GenerateCategoryID()
        {
            return 0;
        }

        public virtual DbSet<CardDetail> CardDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
     var config = builder.Build();
     var connectionString = config.GetConnectionString("QuickKartDBConnectionString");
     if (!optionsBuilder.IsConfigured)
     {
           // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
           optionsBuilder.UseSqlServer(connectionString);
     }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardDetail>(entity =>
            {
                entity.HasKey(e => e.CardNumber)
                    .HasName("pk_CardNumber");

                entity.Property(e => e.CardNumber).HasColumnType("numeric(16, 0)");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CardType)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cvvnumber)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("CVVNumber");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.NameOnCard)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "uq_CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "ix_CategoryId");

                entity.HasIndex(e => e.ProductName, "uq_ProductName")
                    .IsUnique();

                entity.Property(e => e.ProductId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("numeric(8, 0)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_CategoryId");
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.PurchaseId)
                    .HasName("pk_PurchaseId");

                entity.HasIndex(e => e.EmailId, "ix_EmailId");

                entity.HasIndex(e => e.Product, "ix_ProductId");

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.EmailId)
                    .HasConstraintName("fk_EmailId");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.Product)
                    .HasConstraintName("fk_ProductId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "uq_RoleName")
                    .IsUnique();

                entity.Property(e => e.RoleId).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmailId)
                    .HasName("pk_EmailId");

                entity.HasIndex(e => e.RoleId, "ix_RoleId");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_RoleId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
