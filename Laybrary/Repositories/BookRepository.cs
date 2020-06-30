using Laybrary.Models;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laybrary.Repositories
{
    public class BookRepository
    {
        private List<Book> GetAllBooks()
        {
            using (Context db = new Context())
            {
                return db.Books.OrderBy(b => b.Queue).ToList();
            }
        }

        private List<Book> SearchBooks(string _title, string _author, DateTime _registrationDate, string _translation, string _note, int _statusId, int _genreId, int _sourceId)
        {
            using (Context db = new Context())
            {
                return db.Books.Where(b => b.Status_Id == _statusId && (
                                      b.Author.Contains(_author) ||
                                      b.Registration_Date == _registrationDate.Date ||
                                      b.Translation == _translation ||
                                      b.Note.Contains(_note) ||
                                      b.Title.Contains(_title) ||
                                      b.Genre_Id == _genreId ||
                                      b.Source_Id == _sourceId)).OrderBy(b => b.Queue).ToList();
            }
        }

        private Book GetBook(int bookId)
        {
            using (Context db = new Context())
            {
                return db.Books.Single(b => b.Id == bookId);
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

        public DataTable SearchBooksOnDataTable(string title, string author, DateTime registrationDate, string translation, string note, int statusId, int genreId, int sourceId)
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
