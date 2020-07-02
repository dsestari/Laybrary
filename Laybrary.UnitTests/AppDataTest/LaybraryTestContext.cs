namespace Laybrary.UnitTests.AppDataTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LaybraryTestContext : DbContext
    {
        public LaybraryTestContext()
            : base("name=LaybraryTestContext")
        {
        }

        public virtual DbSet<BookCollection> BookCollections { get; set; }
        public virtual DbSet<BookGenre> BookGenres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookSery> BookSeries { get; set; }
        public virtual DbSet<BookSeriesStatu> BookSeriesStatus { get; set; }
        public virtual DbSet<BookSource> BookSources { get; set; }
        public virtual DbSet<BookStatu> BookStatus { get; set; }
        public virtual DbSet<ReadingHistory> ReadingHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCollection>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BookCollection>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<BookCollection>()
                .Property(e => e.Publisher)
                .IsUnicode(false);

            modelBuilder.Entity<BookCollection>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<BookCollection>()
                .Property(e => e.Local)
                .IsUnicode(false);

            modelBuilder.Entity<BookGenre>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BookGenre>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.BookGenre)
                .HasForeignKey(e => e.Genre_Id);

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Translation)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookCollections)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.Book_Id);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.ReadingHistories)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.Book_Id);

            modelBuilder.Entity<BookSery>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BookSery>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<BookSeriesStatu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BookSeriesStatu>()
                .HasMany(e => e.BookSeries)
                .WithOptional(e => e.BookSeriesStatu)
                .HasForeignKey(e => e.SerieStatus_Id);

            modelBuilder.Entity<BookSource>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BookSource>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.BookSource)
                .HasForeignKey(e => e.Source_Id);

            modelBuilder.Entity<BookStatu>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<BookStatu>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.BookStatu)
                .HasForeignKey(e => e.Status_Id);
        }
    }
}
