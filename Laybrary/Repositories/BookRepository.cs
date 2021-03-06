using Laybrary.Models;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laybrary.Repositories
{
    public class BookRepository
    {
        private List<BookModel> GetAllBooks()
        {
            using (Context db = new Context())
            {
                List<BookModel> listModel = new List<BookModel>();
                var model = db.Books.OrderBy(b => b.Queue).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookModel { Id = item.Id,
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
                                                  Source_Id = item.Source_Id});
                }
                return listModel;
            }
        }

        private List<BookModel> SearchBooks(string _title, string _author, DateTime? _registrationDate, string _translation, string _note, int _statusId, int _genreId, int _sourceId)
        {
            using (Context db = new Context())
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
                    result = result.Where(b => b.Registration_Date.HasValue == true && _registrationDate.HasValue == true
                        && b.Registration_Date.Value.Day == _registrationDate.Value.Day
                        && b.Registration_Date.Value.Month == _registrationDate.Value.Month
                        && b.Registration_Date.Value.Year == _registrationDate.Value.Year);
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
            using (Context db = new Context())
            {
                if (book.Id == 0)
                {
                    db.Books.Add(book);
                }
                else
                {
                    var bookInDb = db.Books.Single(b => b.Id == book.Id);
                    if (!String.IsNullOrEmpty(book.Title))
                    {
                        bookInDb.Title = book.Title;
                    }
                    else
                    {
                        book.Title = bookInDb.Title;
                    }

                    if (!String.IsNullOrEmpty(book.Author))
                    {
                        bookInDb.Author = book.Author;
                    }
                    else
                    {
                        book.Author = bookInDb.Author;
                    }

                    if (book.Registration_Date != null)
                    {
                        bookInDb.Registration_Date = book.Registration_Date;
                    }
                    else
                    {
                        book.Registration_Date = bookInDb.Registration_Date;
                    }

                    if (!String.IsNullOrEmpty(book.Translation))
                    {
                        bookInDb.Translation = book.Translation;
                    }
                    else
                    {
                        book.Translation = bookInDb.Translation;
                    }

                    if (!String.IsNullOrEmpty(book.Note))
                    {
                        bookInDb.Note = book.Note;
                    }
                    else
                    {
                        book.Note = bookInDb.Note;
                    }

                    if (book.Registration_Order != null)
                    {
                        bookInDb.Registration_Order = book.Registration_Order;
                    }
                    else
                    {
                        book.Registration_Order = bookInDb.Registration_Order;
                    }

                    if (book.Queue != null)
                    {
                        bookInDb.Queue = book.Queue;
                    }
                    else
                    {
                        book.Queue = bookInDb.Queue;
                    }

                    if (book.Series_Id != null)
                    {
                        bookInDb.Series_Id = book.Series_Id;
                    }
                    else
                    {
                        book.Series_Id = bookInDb.Series_Id;
                    }

                    if (book.Status_Id != null)
                    {
                        bookInDb.Status_Id = book.Status_Id;
                    }
                    else
                    {
                        book.Status_Id = bookInDb.Status_Id;
                    }

                    if (book.Source_Id != null)
                    {
                        bookInDb.Source_Id = book.Source_Id;
                    }
                    else
                    {
                        book.Source_Id = bookInDb.Source_Id;
                    }  
                }

                db.SaveChanges();
            }
        }
        
        private int GetBookNextOrder()
        {
            using (Context db = new Context())
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
            using (Context db = new Context())
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
                using (Context db = new Context())
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
                using (Context db = new Context())
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
                using (Context db = new Context())
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
                using (Context db = new Context())
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

        public DataTable SearchBooksOnDataTable(string title, string author, DateTime? registrationDate, string translation, string note, int statusId, int genreId, int sourceId)
        {
            return Helper.ToDataTable(SearchBooks(title, author, registrationDate, translation, note, statusId, genreId, sourceId));
        }

        public void UpdateBookValidation(Book book)
        {
            if (String.IsNullOrEmpty(book.Title))
            {
                MessageBox.Show("The Title is required.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(book.Author))
            {
                MessageBox.Show("The Author is required.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(book.Genre_Id == 0)
            {
                MessageBox.Show("Please select the book genre.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (book.Status_Id == 0)
            {
                MessageBox.Show("Please select the book status.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (book.Source_Id == 0)
            {
                MessageBox.Show("Please select the book source.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {                   
                    Reorder(book);
                    ReorderQueue(book);

                    UpdateBook(book);
                }
                catch (Exception ex)
                {
                    if (book.Id == 0)
                    {
                        MessageBox.Show("It was not possible to insert a new book, details: " + ex.Message + " Better Call Denis lol", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("It was not possible to update the book, details: " + ex.Message + " Better Call Denis lol", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
         
        public int SuggestNextOrder()
        {
            try
            {
                return GetBookNextOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to get next book order :" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error when trying to get the next book queue " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }
    }
}
