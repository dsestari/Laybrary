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
    public class BookStatusService
    {
        private List<BookStatu> GetAllStatus()
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                return db.BookStatus.ToList();
            }
        }

        private BookStatu GetBookStatus(string status)
        {
            using (LaybraryTestContext db = new LaybraryTestContext())
            {
                return db.BookStatus.Where(bs => bs.Status == status).First();
            }
        }

        public List<String> LoadDropDownListStatus()
        {
            List<String> bookStatus = new List<String>();
            var model = GetAllStatus();

            if (model != null)
            {
                foreach (var item in model)
                {
                    bookStatus.Add(item.Status);
                }
            }
            else
            {
                bookStatus.Add("TBR");
                bookStatus.Add("Read");
                bookStatus.Add("Deleted");
                bookStatus.Add("Collectable");
            }

            return bookStatus;
        }

        public DataTable LoadGridStatus()
        {
           return Helper.ToDataTable(GetAllStatus());
        }
    }
}
