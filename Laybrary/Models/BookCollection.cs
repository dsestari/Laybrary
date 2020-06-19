namespace Laybrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookCollection")]
    public partial class BookCollection
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("Registration Order")]
        public int? Registration_Order { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Column("Number of Pages")]
        public int? Number_of_Pages { get; set; }

        [Required]
        [StringLength(100)]
        public string Publisher { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string Local { get; set; }

        [Column("Purchase Date")]
        public DateTime? Purchase_Date { get; set; }

        public int? Book_Id { get; set; }

        public virtual Book Book { get; set; }
    }
}
