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
    public class BookSerieStatus
    {
        private List<BookSeriesStatu> GetAllSeriesStatus()
        {
            using (Context db = new Context())
            {
                return db.BookSeriesStatus.ToList();
            }
        }

        private BookSeriesStatu GetBookSerieStatus(string description)
        {
            using (Context db = new Context())
            {
                return db.BookSeriesStatus.Where(bss => bss.Description == description).First();
            }
        }

        private void DeleteBookSerieStatus(string description)
        {
            var model = GetBookSerieStatus(description);

            if (model != null)
            {
                using (Context db = new Context())
                {
                    db.BookSeriesStatus.Remove(model);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Error when trying to delete book serie status. Call Denis to fix it!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DeleteBookSerieStatus(description);
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
                int exist = GetBookSerieStatus(bookSerieStatus.Description).Id;

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
