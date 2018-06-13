namespace ToyStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TOrder")]
    public partial class TOrder
    {
        [Key]
        public int idOrder { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [NotMapped]
        [DisplayName("На сумму")]
        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Column("_idProduct")]
        public int C_idProduct { get; set; }

        [Column("_idBuyer")]
        public int C_idBuyer { get; set; }

        public virtual TBuyer TBuyer { get; set; }

        public virtual TProduct TProduct { get; set; }
    }
}
