using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.Models
{
    public class BookCollectionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Registration_Order { get; set; }

        public string Author { get; set; }

        public int? Number_of_Pages { get; set; }

        public string Publisher { get; set; }

        public decimal? Amount { get; set; }

        public string Local { get; set; }

        public DateTime? Purchase_Date { get; set; }

        public int? Book_Id { get; set; }
    }
}
