namespace ToyStore.Areas.Access.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBuyer")]
    public partial class TBuyer
    {
        [Key]
        public int idBuyer { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Column("_TRole")]
        public int? C_TRole { get; set; }

        public virtual TRole TRole { get; set; }
    }
}
