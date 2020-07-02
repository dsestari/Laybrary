namespace Laybrary.UnitTests.AppDataTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReadingHistory")]
    public partial class ReadingHistory
    {
        public int Id { get; set; }

        public int? Book_Id { get; set; }

        [Column("Start Date")]
        public DateTime? Start_Date { get; set; }

        [Column("End Date")]
        public DateTime? End_Date { get; set; }

        public virtual Book Book { get; set; }
    }
}
