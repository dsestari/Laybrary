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
    public class BookGenreService
    {
        private List<BookGenreModel> GetAllGenres()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                List<BookGenreModel> listModel = new List<BookGenreModel>();
                var model = db.BookGenres.OrderBy(g => g.Description).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookGenreModel { Id = item.Id, Description = item.Description });
                }
                return listModel;
            }
        }

        private int GetBookGenreId(string description)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                var model = db.BookGenres.SingleOrDefault(g => g.Description == description);

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

        private bool DeleteBookGenre(string description)
        {
            int Id = GetBookGenreId(description);

            if (Id != 0)
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var model = db.BookGenres.SingleOrDefault(g => g.Id == Id);
                    db.BookGenres.Remove(model);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void AddNewBookGenre(BookGenre bookGenre)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                db.BookGenres.Add(bookGenre);
                db.SaveChanges();
            }
        }

        public List<String> LoadDropDownListGenres()
        {
            List<String> bookGenres = new List<String>();
            var model = GetAllGenres();

            if (model != null)
            {
                foreach (var item in model)
                {
                    bookGenres.Add(item.Description);
                }
            }

            return bookGenres;
        }

        public DataTable LoadGridGenres()
        {
            return Helper.ToDataTable(GetAllGenres());
        }

        public int Count()
        {
            return (int)GetAllGenres().Count();
        }

        public bool DeleteBookGenreValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    var result = DeleteBookGenre(description);

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

        public bool AddNewBookGenreValidation(BookGenre bookGenre)
        {
            if (!String.IsNullOrEmpty(bookGenre.Description))
            {
                int exist = GetBookGenreId(bookGenre.Description);

                if (exist != 0)
                {
                   return false;
                }
                else
                {
                    try
                    {
                        AddNewBookGenre(bookGenre);
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
