namespace ToyStore.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ToyStoreContext : DbContext
    {
        public ToyStoreContext()
            : base("name=ToyStoreContext")
        {
        }

        public virtual DbSet<TBuyer> TBuyers { get; set; }
        public virtual DbSet<TCategorie> TCategories { get; set; }
        public virtual DbSet<TOrder> TOrders { get; set; }
        public virtual DbSet<TProduct> TProducts { get; set; }
        public virtual DbSet<TRole> TRoles { get; set; }
        public virtual DbSet<TSubcategory> TSubcategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBuyer>()
                .HasMany(e => e.TOrders)
                .WithRequired(e => e.TBuyer)
                .HasForeignKey(e => e.C_idBuyer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TCategorie>()
                .HasMany(e => e.TProducts)
                .WithRequired(e => e.TCategorie)
                .HasForeignKey(e => e.C_idCategorie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TProduct>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TProduct>()
                .HasMany(e => e.TOrders)
                .WithRequired(e => e.TProduct)
                .HasForeignKey(e => e.C_idProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRole>()
                .HasMany(e => e.TBuyers)
                .WithOptional(e => e.TRole)
                .HasForeignKey(e => e.C_TRole);

            modelBuilder.Entity<TSubcategory>()
                .HasMany(e => e.TProducts)
                .WithRequired(e => e.TSubcategory)
                .HasForeignKey(e => e.C_idSubcategory)
                .WillCascadeOnDelete(false);
        }
    }
}
