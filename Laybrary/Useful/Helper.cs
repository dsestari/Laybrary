using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laybrary.Useful
{
    public static class Helper
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable("DataTable");
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();

            //Inspect the properties and create the columns in the DataTable
            try
            {
                foreach (PropertyInfo pi in pia)
                {
                    Type ColumnType = pi.PropertyType;
                    if ((ColumnType.IsGenericType))
                    {
                        ColumnType = ColumnType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(pi.Name, ColumnType);
                }
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show("Error when trying to convert List property to DataTable column: " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DuplicateNameException ex)
            {
                MessageBox.Show("Error when trying to convert List property to DataTable column (Duplicate Column or Property): " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show("Error when trying to convert List property to DataTable column: " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to convert List property to DataTable column: " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Populate the data table
            try
            {
                foreach (T item in collection)
                {
                    DataRow dr = dt.NewRow();
                    dr.BeginEdit();
                    foreach (PropertyInfo pi in pia)
                    {
                        if (pi.GetValue(item, null) != null)
                        {
                            dr[pi.Name] = pi.GetValue(item, null);
                        }
                    }
                    dr.EndEdit();
                    dt.Rows.Add(dr);
                }               
            }
            catch (InRowChangingEventException ex)
            {
                MessageBox.Show("Error when trying to edit DataTable: " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch (DeletedRowInaccessibleException ex)
            {
                MessageBox.Show("Error when trying to edit DataTable (Row Inaccessible): " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to populate DataTable: " + ex.Message + " Please contact Denis.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }
}
