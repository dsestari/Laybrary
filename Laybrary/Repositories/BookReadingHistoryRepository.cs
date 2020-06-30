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
    public class BookReadingHistoryRepository
    {
        private List<ReadingHistory> GetBookHistory(int bookId)
        {
            using (Context db = new Context())
            {
                return db.ReadingHistories.Where(h => h.Book_Id == bookId).OrderByDescending(h => h.Id).ToList();
            }
        }

        private void UpdateBookHistory(ReadingHistory readingHist)
        {
            using (Context db = new Context())
            {
                if (readingHist.Id != 0)
                {
                    var model = db.ReadingHistories.SingleOrDefault(h => h.Id == readingHist.Id);

                    if (model != null)
                    {
                        model.Start_Date = readingHist.Start_Date;
                        model.End_Date = readingHist.End_Date;
                        model.Book_Id = readingHist.Book_Id;
                    }
                }
                else
                {
                    db.ReadingHistories.Add(readingHist);
                }

                db.SaveChanges();
            }
        }

        public DataTable LoadBookHistoryOnDataGrid(int bookId)
        {
            return Helper.ToDataTable(GetBookHistory(bookId));
        }

        public void UpdateBookHistoryValidation(ReadingHistory readingHist)
        {
            if (readingHist.Start_Date != null && readingHist.Book_Id != 0)
            {
                try
                {
                    UpdateBookHistory(readingHist);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error trying to update book reading history, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The start date and book id is required", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
