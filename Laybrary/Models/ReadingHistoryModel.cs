using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.Models
{
    public class ReadingHistoryModel
    {
        public int Id { get; set; }

        public int? Book_Id { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }
    }
}
