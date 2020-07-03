using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime? Registration_Date { get; set; }

        public string Translation { get; set; }

        public string Note { get; set; }

        public int? Registration_Order { get; set; }
        
        public int? Queue { get; set; }

        public int? Series_Id { get; set; }

        public int? Status_Id { get; set; }

        public int? Genre_Id { get; set; }

        public int? Source_Id { get; set; }
    }
}
