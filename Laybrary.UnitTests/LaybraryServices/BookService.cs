using Laybrary.UnitTests.AppDataTest;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.UnitTests.LaybraryServices
{
    public class BookService
    {
        private List<BookModel> GetAllBooks()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                List<BookModel> listModel = new List<BookModel>();
                var model = db.Books.OrderBy(b => b.Queue).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Author = item.Author,
                        Registration_Date = item.Registration_Date,
                        Translation = item.Translation,
                        Note = item.Note,
                        Registration_Order = item.Registration_Order,
                        Queue = item.Queue,
                        Series_Id = item.Series_Id,
                        Status_Id = item.Status_Id,
                        Genre_Id = item.Genre_Id,
                        Source_Id = item.Source_Id
                    });
                }
                return listModel;
            }
        }

        private List<BookModel> SearchBooks(string _title, string _author, DateTime _registrationDate, string _translation, string _note, int _statusId, int _genreId, int _sourceId)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                List<BookModel> listModel = new List<BookModel>();
                IQueryable<Book> result = db.Books;

                if (!String.IsNullOrEmpty(_title))
                {
                    result = result.Where(b => b.Title.Contains(_title));
                }

                if (!String.IsNullOrEmpty(_author))
                {
                    result = result.Where(b => b.Author.Contains(_author));
                }

                if (_registrationDate != null)
                {
                    result = result.Where(b => b.Registration_Date == _registrationDate);
                }

                if (!String.IsNullOrEmpty(_translation))
                {
                    result = result.Where(b => b.Translation.Contains(_translation));
                }

                if (!String.IsNullOrEmpty(_note))
                {
                    result = result.Where(b => b.Note.Contains(_note));
                }

                if (_statusId != 0)
                {
                    result = result.Where(b => b.Status_Id == _statusId);
                }

                if (_genreId != 0)
                {
                    result = result.Where(b => b.Genre_Id == _genreId);
                }

                if (_sourceId != 0)
                {
                    result = result.Where(b => b.Source_Id == _sourceId);
                }

                var model = result.OrderBy(b => b.Registration_Order).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Author = item.Author,
                        Registration_Date = item.Registration_Date,
                        Translation = item.Translation,
                        Note = item.Note,
                        Registration_Order = item.Registration_Order,
                        Queue = item.Queue,
                        Series_Id = item.Series_Id,
                        Status_Id = item.Status_Id,
                        Genre_Id = item.Genre_Id,
                        Source_Id = item.Source_Id
                    });
                }
                return listModel;
            }
        }

        private void UpdateBook(Book book)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                if (book.Id == 0)
                {
                    db.Books.Add(book);
                }
                else
                {
                    var bookInDb = db.Books.Single(b => b.Id == book.Id);
                    bookInDb.Title = book.Title;
                    bookInDb.Author = book.Author;
                    bookInDb.Registration_Date = DateTime.Now.Date;
                    bookInDb.Translation = book.Translation;
                    bookInDb.Note = book.Note;
                    bookInDb.Registration_Order = book.Registration_Order;
                    bookInDb.Queue = book.Queue;
                    bookInDb.Series_Id = book.Series_Id;
                    bookInDb.Status_Id = book.Status_Id;
                    bookInDb.Source_Id = book.Source_Id;
                }

                db.SaveChanges();
            }
        }

        private int GetBookNextOrder()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                var lastOrder = db.Books.OrderByDescending(b => b.Registration_Order).Select(b => b.Registration_Order).First();

                if (lastOrder != null)
                {
                    return (int)lastOrder + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private int GetBookNextOnTheQueue()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                var lastQueue = db.Books.OrderByDescending(b => b.Queue).Select(b => b.Queue).First();

                if (lastQueue != null)
                {
                    return (int)lastQueue + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private void Reorder(Book book)
        {
            if (book.Id == 0)
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var booksToReorder = db.Books.Where(b => b.Registration_Order >= book.Registration_Order).ToList();

                    if (booksToReorder != null)
                    {
                        foreach (var item in booksToReorder)
                        {
                            item.Registration_Order = item.Registration_Order + 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var booksToReorderAfterNewIndex = db.Books.Where(b => b.Registration_Order >= book.Registration_Order).ToList();

                    if (booksToReorderAfterNewIndex != null)
                    {
                        foreach (var item in booksToReorderAfterNewIndex)
                        {
                            item.Registration_Order = item.Registration_Order + 1;
                            db.SaveChanges();
                        }
                    }

                    var booksToReorderBeforeNewIndex = db.Books.Where(b => b.Registration_Order < book.Registration_Order).ToList();

                    if (booksToReorderBeforeNewIndex != null)
                    {
                        foreach (var item in booksToReorderBeforeNewIndex)
                        {
                            item.Registration_Order = item.Registration_Order - 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        private void ReorderQueue(Book book)
        {
            if (book.Id == 0)
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var booksToReorder = db.Books.Where(b => b.Queue >= book.Queue).ToList();

                    if (booksToReorder != null)
                    {
                        foreach (var item in booksToReorder)
                        {
                            item.Queue = item.Queue + 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var booksToReorderAfterNewIndex = db.Books.Where(b => b.Queue >= book.Queue).ToList();

                    if (booksToReorderAfterNewIndex != null)
                    {
                        foreach (var item in booksToReorderAfterNewIndex)
                        {
                            item.Queue = item.Queue + 1;
                            db.SaveChanges();
                        }
                    }

                    var booksToReorderBeforeNewIndex = db.Books.Where(b => b.Queue < book.Queue).ToList();

                    if (booksToReorderBeforeNewIndex != null)
                    {
                        foreach (var item in booksToReorderBeforeNewIndex)
                        {
                            item.Queue = item.Queue - 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public DataTable GetAllBooksOnDataTable()
        {
            return Helper.ToDataTable(GetAllBooks());
        }

        public DataTable SearchBooksOnDataTable(string title, string author, DateTime registrationDate, string translation, string note, int statusId, int genreId, int sourceId)
        {
            return Helper.ToDataTable(SearchBooks(title, author, registrationDate, translation, note, statusId, genreId, sourceId));
        }

        public bool UpdateBookValidation(Book book)
        {
            if (String.IsNullOrEmpty(book.Title))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(book.Author))
            {
                return false;
            }
            else
            {
                try
                {
                    Reorder(book);
                    ReorderQueue(book);

                    UpdateBook(book);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int SuggestNextOrder()
        {
            try
            {
                return GetBookNextOrder();
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int SuggestNextOnTheQueue()
        {
            try
            {
                return GetBookNextOnTheQueue();
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
