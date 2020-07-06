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
    }
}
