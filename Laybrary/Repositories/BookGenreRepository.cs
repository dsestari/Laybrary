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
    public class BookGenreRepository
    {
        private List<BookGenreModel> GetAllGenres()
        {            
            using (Context db = new Context())
            {
                List<BookGenreModel> listModel = new List<BookGenreModel>();
                var model = db.BookGenres.OrderBy(g => g.Description).ToList();

                foreach (var item in model)
                {
                    listModel.Add(new BookGenreModel { Id = item.Id, Description = item.Description});
                }
                return listModel;
            }
        }

        private BookGenre GetBookGenre(string description)
        {
            using (Context db = new Context())
            {
                return db.BookGenres.Where(g => g.Description == description).First();
            }
        }

        private void DeleteBookGenre(string description)
        {
            var model = GetBookGenre(description);

            if (model != null)
            {
                using (Context db = new Context())
                {
                    db.BookGenres.Remove(model);
                    db.SaveChanges();
                }
            }
        }

        private void AddNewBookGenre(BookGenre bookGenre)
        {
            using (Context db = new Context())
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

        public void DeleteBookGenreValidation(string description)
        {
            if (!String.IsNullOrEmpty(description))
            {
                try
                {
                    DeleteBookGenre(description);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to delete book genre, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one genre", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AddNewBookGenreValidation(BookGenre bookGenre)
        {
            if (!String.IsNullOrEmpty(bookGenre.Description))
            {
                int exist = GetBookGenre(bookGenre.Description).Id;

                if (exist != 0)
                {
                    MessageBox.Show("The genre already exist!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        AddNewBookGenre(bookGenre);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when trying to add new genre, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please type the new book genre. ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
