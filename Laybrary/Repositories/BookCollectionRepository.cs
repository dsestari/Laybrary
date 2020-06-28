using Laybrary.Models;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<BookCollection> SearchCollection(string _name, string _author, int _numOfPages, string _publisher, decimal _amount, string _local, DateTime _purchaseDate)
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

                return result.ToList();
            }
        } 

        public DataTable LoadCollectionOnDataGrid()
        {
            return Helper.ToDataTable(GetCollection());
        }
    }
}
