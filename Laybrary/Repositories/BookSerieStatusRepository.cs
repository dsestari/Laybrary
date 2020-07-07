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
    public class BookSerieStatusRepository
    {
        private List<BookSeriesStatusModel> GetAllSeriesStatus()
        {
            using (Context db = new Context())
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
            using (Context db = new Context())
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

        private bool DeleteBookSerieStatus(string description)
        {
            int Id = GetBookSerieStatusId(description);

            if (Id != 0)
            {
                using (Context db = new Context())
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

        private void AddNewBookSerieStatus(BookSeriesStatu bookSerieStatus)
        {
            using (Context db = new Context())
            {
                db.BookSeriesStatus.Add(bookSerieStatus);
                db.SaveChanges();
            }
        }

        public List<String> LoadDropDownListSerieStatus()
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

        public void DeleteBookSerieStatusValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    var result = DeleteBookSerieStatus(description);

                    if (result != true)
                    {
                        MessageBox.Show("It was not possible to delete the series status. The status " + description + " not found.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Status deleted with success!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to delete serie status, details:" + ex.Message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one series status", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AddNewBookSerieStatusValidation(BookSeriesStatu bookSerieStatus)
        {
            if (!String.IsNullOrEmpty(bookSerieStatus.Description))
            {
                int exist = GetBookSerieStatusId(bookSerieStatus.Description);

                if (exist != 0)
                {
                    MessageBox.Show("The status already exist!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        AddNewBookSerieStatus(bookSerieStatus);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when trying to add new status, details:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Please type the new status. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
