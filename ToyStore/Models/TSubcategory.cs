namespace ToyStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TSubcategory")]
    public partial class TSubcategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TSubcategory()
        {
            TProducts = new HashSet<TProduct>();
        }

        [Key]
        public int idSubcategory { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Подкатегория")]
        public string Subcategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TProduct> TProducts { get; set; }
    }
}
