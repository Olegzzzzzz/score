namespace ToyStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBuyer")]
    public partial class TBuyer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBuyer()
        {
            TOrders = new HashSet<TOrder>();
        }

        [Key]
        public int idBuyer { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Имя")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Column("_TRole")]
        public int? C_TRole { get; set; }

        public virtual TRole TRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOrder> TOrders { get; set; }
    }
}
