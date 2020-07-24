using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laybrary.Models;
using System.Windows.Forms;
using System.Data;
using Laybrary.Useful;

namespace Laybrary.Repositories
{
    public class BookSeriesRepository
    {
        private List<BookSeriesModel> GetAllSeries()
        {
            using (Context db = new Context())
            {
                List<BookSeriesModel> listModel = new List<BookSeriesModel>();
                var model = db.BookSeries.OrderBy(bs => bs.Queue).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookSeriesModel { Id = item.Id,
                                                        Name = item.Name,
                                                        Author = item.Author,
                                                        Registration_Order = item.Registration_Order,
                                                        Total_Books = item.Total_Books,
                                                        Total_Read= item.Total_Read,
                                                        Queue = item.Queue,
                                                        SerieStatus_Id = item.SerieStatus_Id});
                }
                return listModel;
            }
        }

        private List<BookSeriesModel> SearchSeries(string _seriesName, string _author, int _seriesStatusId)
        {
            using (Context db = new Context())
            {
                List<BookSeriesModel> listModel = new List<BookSeriesModel>();
                IQueryable<BookSery> result = db.BookSeries;

                if (!String.IsNullOrEmpty(_seriesName))
                {
                    result = result.Where(bs => bs.Name.Contains(_seriesName));
                }

                if (!String.IsNullOrEmpty(_author))
                {
                    result = result.Where(bs => bs.Author.Contains(_author));
                }

                if (_seriesStatusId != 0)
                {
                    result = result.Where(bs => bs.SerieStatus_Id == _seriesStatusId);
                }

                var model = result.OrderBy(bs => bs.Queue).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookSeriesModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Author = item.Author,
                        Registration_Order = item.Registration_Order,
                        Total_Books = item.Total_Books,
                        Total_Read = item.Total_Read,
                        Queue = item.Queue,
                        SerieStatus_Id = item.SerieStatus_Id
                    });
                }
                return listModel;
            }
        }

        private int GetNextOrder()
        {
            using (Context db = new Context())
            {
                var lastOrder = db.BookSeries.OrderByDescending(bs => bs.Registration_Order).Select(b => b.Registration_Order).First();

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

        private int GetNextOnTheQueue()
        {
            using (Context db = new Context())
            {
                var lastOnTheQ = db.BookSeries.OrderByDescending(bs => bs.Queue).Select(bs => bs.Queue).First();

                if (lastOnTheQ != null)
                {
                    return (int)lastOnTheQ + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private int GetBookSeriesId(string author, string seriesName)
        {
            using (Context db = new Context())
            {
                var model = db.BookSeries.SingleOrDefault(bs => bs.Author == author && bs.Name == seriesName);

                if (model != null)
                {
                    return model.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        private int GetTotalNumberOfBooksPerSeries(string author, string seriesName)
        {
            var Id = GetBookSeriesId(author, seriesName);

            if (Id != 0)
            {
                using (Context db = new Context())
                {
                    return db.Books.Where(bs => bs.Series_Id == Id).Count();
                }
            }
            else
            {
                return 0;
            }
        }

        private int GetTotalNumberOfBooksPerSeriesRead(string author, string seriesName)
        {
            var serieId = GetBookSeriesId(author, seriesName);

            if (serieId != 0)
            {
                using (Context db = new Context())
                {
                    return db.Books.Join(db.ReadingHistories, b => b.Id, rh => rh.Book_Id, (b, rh) => new { Book = b, ReadingHistory = rh })
                        .Where(bAndRh => bAndRh.Book.Id == bAndRh.ReadingHistory.Book_Id)
                        .Where(x => x.Book.Series_Id == serieId).Count();
                }
            }
            else
            {
                return 0;
            }
        }

        private void UpdateBookSeries(BookSery bookSeries)
        {
            using (Context db = new Context())
            {
                if (bookSeries.Id == 0)
                {
                    db.BookSeries.Add(bookSeries);
                }
                else
                {                    
                    var model = db.BookSeries.Single(bs => bs.Id == bookSeries.Id);

                    if (!String.IsNullOrEmpty(bookSeries.Name))
                    {
                        model.Name = bookSeries.Name;
                    }
                    else
                    {
                        bookSeries.Name = model.Name;
                    }

                    if (!String.IsNullOrEmpty(bookSeries.Author))
                    {
                        model.Author = bookSeries.Author;
                    }
                    else
                    {
                        bookSeries.Author = model.Author;
                    }

                    if (bookSeries.Queue != null)
                    {
                        model.Queue = bookSeries.Queue;
                    }
                    else
                    {
                        bookSeries.Queue = model.Queue;
                    }

                    if (bookSeries.Registration_Order != null)
                    {
                        model.Registration_Order = bookSeries.Registration_Order;
                    }
                    else
                    {
                        bookSeries.Registration_Order = model.Registration_Order;
                    }

                    if (bookSeries.Total_Books != null)
                    {
                        model.Total_Books = bookSeries.Total_Books;
                    }
                    else
                    {
                        bookSeries.Total_Books = model.Total_Books;
                    }

                    if (bookSeries.Total_Read != null)
                    {
                        model.Total_Read = bookSeries.Total_Read;
                    }
                    else
                    {
                        bookSeries.Total_Read = model.Total_Read;
                    }

                    if (bookSeries.SerieStatus_Id != null)
                    {
                        model.SerieStatus_Id = bookSeries.SerieStatus_Id;
                    }
                    else
                    {
                        bookSeries.SerieStatus_Id = model.SerieStatus_Id;
                    }  
                }

                db.SaveChanges();
            }
        }           

        private void Reorder(BookSery bookSeries)
        {
            if (bookSeries.Id == 0)
            {
                using (Context db = new Context())
                {
                    var booksSeriesToReorder = db.BookSeries.Where(bs => bs.Registration_Order >= bookSeries.Registration_Order).ToList();

                    if (booksSeriesToReorder != null)
                    {
                        foreach (var item in booksSeriesToReorder)
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
                    var booksSeriesToReorderAfterNewIndex = db.BookSeries.Where(bs => bs.Registration_Order >= bookSeries.Registration_Order).ToList();

                    if (booksSeriesToReorderAfterNewIndex != null)
                    {
                        foreach (var item in booksSeriesToReorderAfterNewIndex)
                        {
                            item.Registration_Order = item.Registration_Order + 1;
                            db.SaveChanges();
                        }
                    }

                    var booksSeriesToReorderBeforeNewIndex = db.BookSeries.Where(bs => bs.Registration_Order < bookSeries.Registration_Order).ToList();

                    if (booksSeriesToReorderBeforeNewIndex != null)
                    {
                        foreach (var item in booksSeriesToReorderBeforeNewIndex)
                        {
                            item.Registration_Order = item.Registration_Order - 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        private void ReorderQueue(BookSery bookSeries)
        {
            if (bookSeries.Id == 0)
            {
                using (Context db = new Context())
                {
                    var booksSeriesToReorder = db.BookSeries.Where(bs => bs.Queue >= bookSeries.Queue).ToList();

                    if (booksSeriesToReorder != null)
                    {
                        foreach (var item in booksSeriesToReorder)
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
                    var booksSeriesToReorderAfterNewIndex = db.BookSeries.Where(bs => bs.Queue >= bookSeries.Queue).ToList();

                    if (booksSeriesToReorderAfterNewIndex != null)
                    {
                        foreach (var item in booksSeriesToReorderAfterNewIndex)
                        {
                            item.Queue = item.Queue + 1;
                            db.SaveChanges();
                        }
                    }

                    var booksSeriesToReorderBeforeNewIndex = db.BookSeries.Where(b => b.Queue < bookSeries.Queue).ToList();

                    if (booksSeriesToReorderBeforeNewIndex != null)
                    {
                        foreach (var item in booksSeriesToReorderBeforeNewIndex)
                        {
                            item.Queue = item.Queue - 1;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }          

        public DataTable LoadAllSeriesOnDataGrid()
        {
            return Helper.ToDataTable(GetAllSeries());
        }

        public DataTable SearchSeriesOnDataTable(string seriesName, string author, int seriesStatusId)
        {
            return Helper.ToDataTable(SearchSeries(seriesName, author, seriesStatusId));
        }

        public int SuggestNextOrder()
        {
            try
            {
                return GetNextOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to get next serie order :" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        public int SuggestNextOnTheQueue()
        {
            try
            {
                return GetNextOnTheQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to get next number on the queue, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        public int LoadTotalBookSeriesTxt(string author, string seriesName)
        {
            if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(seriesName))
            {
                try
                {
                    return GetTotalNumberOfBooksPerSeries(author, seriesName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to get the total of book series, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("Please the author and series name is required. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public int LoadTotalBookSeriesReadTxt(string author, string seriesName)
        {
            if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(seriesName))
            {
                try
                {
                    return GetTotalNumberOfBooksPerSeriesRead(author, seriesName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to get the total of book series, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("Please the author and series name is required. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public void UpdateBookSeriesValidation(BookSery bookSeries)
        {
            if (String.IsNullOrEmpty(bookSeries.Name))
            {
                MessageBox.Show("The book series name is required. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (String.IsNullOrEmpty(bookSeries.Author))
            {

                MessageBox.Show("The book series author is required. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var bookSeriesId = GetBookSeriesId(bookSeries.Author, bookSeries.Name);

                    if (bookSeriesId != 0)
                    {
                        bookSeries.Id = bookSeriesId;
                    }

                    Reorder(bookSeries);
                    ReorderQueue(bookSeries);

                    UpdateBookSeries(bookSeries);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to add new book series. Details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }       
    }
}
