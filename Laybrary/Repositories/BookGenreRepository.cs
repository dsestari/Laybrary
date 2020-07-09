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

        private int GetBookGenreId(string description)
        {
            using (Context db = new Context())
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
                using (Context db = new Context())
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
                    var result = DeleteBookGenre(description);

                    if (result != true)
                    {
                        MessageBox.Show("It was not possible to find the book genre "+ description, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Genre deleted with success");
                    }
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
                int exist = GetBookGenreId(bookGenre.Description);

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
