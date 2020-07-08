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
    public class BookSeriesStatusService
    {
        private List<BookSeriesStatusModel> GetAllSeriesStatus()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                List<BookSeriesStatusModel> listModel = new List<BookSeriesStatusModel>();
                var model = db.BookSeriesStatus.ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookSeriesStatusModel { Id = item.Id, Description = item.Description });
                }
                return listModel;
            }
        }
       
        private int GetBookSerieStatusId(string description)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                var model = db.BookSeriesStatus.SingleOrDefault(bss => bss.Description == description);

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

        private void AddNewBookSerieStatus(BookSeriesStatu bookSerieStatus)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                db.BookSeriesStatus.Add(bookSerieStatus);
                db.SaveChanges();
            }
        }

        private bool DeleteBookSerieStatus(string description)
        {
            int Id = GetBookSerieStatusId(description);

            if (Id != 0)
            {
                using (LaybraryTestContext db = new LaybraryTestContext())
                {
                    var model = db.BookSeriesStatus.SingleOrDefault(bss => bss.Id == Id);
                    db.BookSeriesStatus.Remove(model);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<String> loadDropDownListSeriesStatus()
        {
            List<String> bookSeriesStatus = new List<String>();
            var list = GetAllSeriesStatus();

            if (list != null)
            {
                foreach (var item in list)
                {
                    bookSeriesStatus.Add(item.Description);
                }
            }
            else
            {
                bookSeriesStatus.Add("In Progress");
                bookSeriesStatus.Add("Awaiting Launch");
                bookSeriesStatus.Add("No Started");
                bookSeriesStatus.Add("Abandoned");
                bookSeriesStatus.Add("Concluded");
            }

            return bookSeriesStatus;
        }

        public DataTable LoadGridSeriesStatus()
        {
            return Helper.ToDataTable(GetAllSeriesStatus());
        }

        public int Count()
        {
            return (int)GetAllSeriesStatus().Count();
        }

        public bool AddNewBookSerieStatusValidation(BookSeriesStatu bookSerieStatus)
        {
            if (!String.IsNullOrEmpty(bookSerieStatus.Description))
            {
                int exist = GetBookSerieStatusId(bookSerieStatus.Description);

                if (exist != 0)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        AddNewBookSerieStatus(bookSerieStatus);

                        return true;
                    }
                    catch (Exception)
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

        public bool DeleteBookSerieStatusValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    var result = DeleteBookSerieStatus(description);

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
    }
}
