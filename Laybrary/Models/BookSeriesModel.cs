using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.Models
{
    public class BookSeriesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Registration_Order { get; set; }

        public int? Queue { get; set; }

        public string Author { get; set; }

        public int? Total_Books { get; set; }

        public int? Total_Read { get; set; }

        public int? SerieStatus_Id { get; set; }
    }
}
