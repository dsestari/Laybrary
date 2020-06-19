namespace Laybrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BookSery
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("Registration Order")]
        public int? Registration_Order { get; set; }

        public int? Queue { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Column("Total Books")]
        public int? Total_Books { get; set; }

        [Column("Total Read")]
        public int? Total_Read { get; set; }

        public int? SerieStatus_Id { get; set; }

        public virtual BookSeriesStatu BookSeriesStatu { get; set; }
    }
}
