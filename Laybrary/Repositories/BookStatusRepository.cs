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
    public class BookStatusRepository
    {
        private List<BookStatusModel> GetAllStatus()
        {
            using (Context db = new Context())
            {
                List<BookStatusModel> listModel = new List<BookStatusModel>();
                var model = db.BookStatus.ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookStatusModel { Id = item.Id, Status = item.Status});
                }
                return listModel;
            }
        }

        private int GetBookStatusId(string status)
        {
            using (Context db = new Context())
            {
                var model = db.BookStatus.SingleOrDefault(x => x.Status == status);

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

        private bool DeleteBookStatus(string status)
        {            
            int Id = GetBookStatusId(status);

            if (Id != 0)
            {
                using (Context db = new Context())
                {
                    var model = db.BookStatus.SingleOrDefault(bs => bs.Id == Id);
                    db.BookStatus.Remove(model);
                    db.SaveChanges();

                    return true;
                }
            }
            else
            {
                return false;
            }      
        }

        private void AddNewBookStatus(BookStatu bookStatus)
        {
            using (Context db = new Context())
            {
                db.BookStatus.Add(bookStatus);
                db.SaveChanges();
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
            //return Helper.ToDataTable(GetAllStatus());
            return GetAllStatus().ToDataTable();
        }

        public void DeleteBookStatusValidation(string status)
        {
            if (!String.IsNullOrEmpty(status))
            {
                try
                {
                    var result = DeleteBookStatus(status);

                    if (result != true)
                    {
                        MessageBox.Show("It was not possible to delete the status. Status not found", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Status deleted with success.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to delete book status, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one status", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AddNewBookStatusValidation(BookStatu bookStatus)
        {
            if (!String.IsNullOrEmpty(bookStatus.Status))
            {
                int exist = GetBookStatusId(bookStatus.Status);

                if (exist != 0)
                {
                    MessageBox.Show("The staus already exist!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        AddNewBookStatus(bookStatus);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when trying to add new status, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please type the new book status. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
