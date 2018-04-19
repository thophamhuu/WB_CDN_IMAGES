namespace CreateThumbnail.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }
        public DataContext(string connectionString)
                    : base(connectionString)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Picture_Mapping> Product_Picture_Mapping { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>()
                .Property(e => e.SizeThumbs)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.UpdatedToTicks)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.AdditionalShippingCharge)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.OldPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.MinimumCustomerEnteredPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.MaximumCustomerEnteredPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.BasepriceAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.BasepriceBaseAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Length)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Width)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Height)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.OverriddenGiftCardAmount)
                .HasPrecision(18, 0);
        }
    }
}
