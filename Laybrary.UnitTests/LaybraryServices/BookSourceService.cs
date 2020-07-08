using Laybrary.UnitTests.AppDataTest;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.UnitTests.LaybraryServices
{
    public class BookSourceService
    {
        private List<BookSourceModel> GetAllSources()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                List<BookSourceModel> listModel = new List<BookSourceModel>();
                var model = db.BookSources.ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookSourceModel { Id = item.Id, Description = item.Description });
                }
                return listModel;
            }
        }

        private int GetBookSourceId(string description)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                var model = db.BookSources.SingleOrDefault(bs => bs.Description == description);

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

        private bool DeleteBookSources(string description)
        {
            int Id = GetBookSourceId(description);

            if (Id != 0)
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var model = db.BookSources.SingleOrDefault(bs => bs.Id == Id);
                    db.BookSources.Remove(model);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void AddNewBookSources(BookSource bookSources)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                db.BookSources.Add(bookSources);
                db.SaveChanges();
            }
        }

        public List<String> LoadDropDownListSource()
        {
            List<String> bookSources = new List<String>();
            var model = GetAllSources();

            if (model != null)
            {
                foreach (var item in model)
                {
                    bookSources.Add(item.Description);
                }
            }
            else
            {
                bookSources.Add("Collection");
                bookSources.Add("Ebook");
                bookSources.Add("Library");
            }

            return bookSources;
        }

        public DataTable LoadGridSource()
        {
            return Helper.ToDataTable(GetAllSources());
        }

        public bool DeleteBookSourcesValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    var result = DeleteBookSources(description);

                    if (result != true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AddNewBookSourcesValidation(BookSource bookSources)
        {
            if (!String.IsNullOrEmpty(bookSources.Description))
            {
                int exist = GetBookSourceId(bookSources.Description);

                if (exist != 0)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        AddNewBookSources(bookSources);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
