namespace ToyStore.Areas.Access.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ToyStoreAccessContext : DbContext
    {
        public ToyStoreAccessContext()
            : base("name=ToyStoreAccessContext")
        {
        }

        public virtual DbSet<TBuyer> TBuyers { get; set; }
        public virtual DbSet<TRole> TRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TRole>()
                .HasMany(e => e.TBuyers)
                .WithOptional(e => e.TRole)
                .HasForeignKey(e => e.C_TRole);
        }
    }
}
