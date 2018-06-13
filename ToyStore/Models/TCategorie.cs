namespace ToyStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TCategorie")]
    public partial class TCategorie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TCategorie()
        {
            TProducts = new HashSet<TProduct>();
        }

        [Key]
        public int idCategorie { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Категория")]
        public string Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TProduct> TProducts { get; set; }
    }
}
