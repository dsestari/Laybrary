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
    public class BookSourceRepository
    {
        private List<BookSourceModel> GetAllSources()
        {
            using (Context db = new Context())
            {
                List<BookSourceModel> listModel = new List<BookSourceModel>();
                var model = db.BookSources.ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookSourceModel { Id = item.Id, Description = item.Description});
                }
                return listModel;
            }
        }

        private int GetBookSourceId(string description)
        {
            using (Context db = new Context())
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
                using (Context db = new Context())
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
            using (Context db = new Context())
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

        public void DeleteBookSourcesValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    var result = DeleteBookSources(description);

                    if (result != true)
                    {
                        MessageBox.Show("It was not possible to find the source " + description, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Book source delete with success");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to delete book sources, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one source", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AddNewBookSourcesValidation(BookSource bookSources)
        {
            if (!String.IsNullOrEmpty(bookSources.Description))
            {
                int exist = GetBookSourceId(bookSources.Description);

                if (exist != 0)
                {
                    MessageBox.Show("The source already exist.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        AddNewBookSources(bookSources);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when trying to add new book source, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please type the new book source ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
