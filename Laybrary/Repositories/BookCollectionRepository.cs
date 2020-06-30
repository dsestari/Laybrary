using Laybrary.Models;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laybrary.Repositories
{
    public class BookCollectionRepository
    {
        private List<BookCollection> GetCollection()
        {
            using (Context db = new Context())
            {
                return db.BookCollections.OrderBy(c => c.Registration_Order).ToList();
            }
        }

        private List<BookCollection> SearchCollection(string _name, string _author, int _numOfPages, string _publisher, bool _searchAmount, decimal _amount, string _local, DateTime _purchaseDate)
        {
            using (Context db = new Context())
            {                
                var result = db.BookCollections;

                if (!String.IsNullOrEmpty(_name))
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Name.Contains(_name));
                }

                if (!String.IsNullOrEmpty(_author))
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Author.Contains(_author));
                }

                if (_numOfPages != 0)
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Number_of_Pages == _numOfPages);
                } 

                if (!String.IsNullOrEmpty(_publisher))
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Publisher.Contains(_publisher));
                }

                if (_searchAmount == true)
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Amount == _amount);
                }

                if (!String.IsNullOrEmpty(_local))
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Local.Contains(_local));
                }

                if (_purchaseDate != null)
                {
                    result = (DbSet<BookCollection>)result.Where(c => c.Purchase_Date == _purchaseDate);
                }
                
                return result.OrderBy(c => c.Registration_Order).ToList();
            }
        }

        private int GetNextOrder()
        {
            using (Context db = new Context())
            {
                var lastOrder = db.BookCollections.OrderByDescending(c => c.Registration_Order).Select(c => c.Registration_Order).First();

                if (lastOrder != null)
                {
                    return (int)lastOrder + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private int GetCollectionId(string _name, string _author, string _publisher)
        {
            using (Context db = new Context())
            {
                return db.BookCollections.Single(c => c.Name == _name && c.Author == _author && c.Publisher == _publisher).Id;
            }
        }        

        private void UpdateCollection(BookCollection bookCollection)
        {
            using (Context db = new Context())
            {
                if (bookCollection.Id == 0)
                {
                    db.BookCollections.Add(bookCollection);
                }
                else
                {
                    var model = db.BookCollections.Single(c => c.Id == bookCollection.Id);

                    if (model != null)
                    {
                        model.Name = bookCollection.Name;
                        model.Author = bookCollection.Author;
                        model.Number_of_Pages = bookCollection.Number_of_Pages;
                        model.Publisher = bookCollection.Publisher;
                        model.Amount = bookCollection.Amount;
                        model.Local = bookCollection.Local;
                        model.Purchase_Date = bookCollection.Purchase_Date;
                        model.Book_Id = bookCollection.Book_Id;
                    }
                }

                db.SaveChanges();
            }
        }

        public DataTable LoadCollectionOnDataGrid()
        {
            return Helper.ToDataTable(GetCollection());
        }

        public DataTable SearchCollectionValidation(string name, string author, int numOfPages, string publisher, bool searchAmount, decimal amount, string local, DateTime purchaseDate)
        {
            return Helper.ToDataTable(SearchCollection(name, author, numOfPages, publisher, searchAmount, amount, local, purchaseDate));
        }

        public int SuggestNextOrder()
        {
            try
            {
                return GetNextOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to get next collection order :" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        public int GetCollectionIdValidation(string name, string author, string publisher)
        {
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(publisher))
            {
                try
                {
                    return GetCollectionId(name, author, publisher);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to get Collection Id, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("The name, author and publisher are required to identify the Collection.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public void UpdateCollectionValidation(BookCollection bookCollection)
        {
            if (bookCollection.Registration_Order > 0 && !String.IsNullOrEmpty(bookCollection.Name) && !String.IsNullOrEmpty(bookCollection.Author) && !String.IsNullOrEmpty(bookCollection.Publisher))
            {
                int collectionId = GetCollectionId(bookCollection.Name, bookCollection.Author, bookCollection.Publisher);

                if (collectionId != 0)
                {
                    bookCollection.Id = collectionId;
                }

                try
                {
                    UpdateCollection(bookCollection);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to update collection, details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The Collection Name, Author and Publisher are required and Order should be granter then 0.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
