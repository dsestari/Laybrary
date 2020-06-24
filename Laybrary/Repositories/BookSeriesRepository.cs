using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laybrary.Models;
using System.Windows.Forms;

namespace Laybrary.Repositories
{
    public class BookSeriesRepository
    {
        private int GetNextOrder()
        {
            using (Context db = new Context())
            {
                var lastOrder = db.BookSeries.OrderByDescending(bs => bs.Registration_Order).Select(b => b.Registration_Order).First();

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

        public int SuggestNextOrder()
        {
            try
            {
                return GetNextOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to get next serie order :" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}
