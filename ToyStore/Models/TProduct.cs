namespace ToyStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TProduct")]
    public partial class TProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TProduct()
        {
            TOrders = new HashSet<TOrder>();
        }

        [Key]
        public int idProduct { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Наименование")]
        [UIHint("MultilineText")]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Описание")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [NotMapped]
        public string SmallDescription { get; set; }


        [StringLength(100)]
        public string Image { get; set; }

        [DisplayName("Изображение")]
        public byte[] Picture { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Производитель")]
        public string Manufacturer { get; set; }

        [Column("_idSubcategory")]
       
        public int C_idSubcategory { get; set; }

        [Column("_idCategorie")]
        
        public int C_idCategorie { get; set; }

        public virtual TCategorie TCategorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOrder> TOrders { get; set; }

        public virtual TSubcategory TSubcategory { get; set; }

        [NotMapped]
        public int NumberOfPieces { get; set; }
    }
}
