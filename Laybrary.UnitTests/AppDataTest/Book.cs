namespace Laybrary.UnitTests.AppDataTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            BookCollections = new HashSet<BookCollection>();
            ReadingHistories = new HashSet<ReadingHistory>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Column("Registration Date")]
        public DateTime? Registration_Date { get; set; }

        [StringLength(100)]
        public string Translation { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [Column("Registration Order")]
        public int? Registration_Order { get; set; }

        public int? Queue { get; set; }

        public int? Series_Id { get; set; }

        public int? Status_Id { get; set; }

        public int? Genre_Id { get; set; }

        public int? Source_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookCollection> BookCollections { get; set; }

        public virtual BookGenre BookGenre { get; set; }

        public virtual BookSource BookSource { get; set; }

        public virtual BookStatu BookStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReadingHistory> ReadingHistories { get; set; }
    }
}
