using Laybrary.Models;
using Laybrary.Useful;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable LoadCollectionOnDataGrid()
        {
            return Helper.ToDataTable(GetCollection());
        }
    }
}
