using Laybrary.UnitTests.AppDataTest;
using Laybrary.UnitTests.LaybraryServices;
using Laybrary.Useful;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laybrary.UnitTests.Scenarios
{
    [TestClass]
    public class BookSeriesStatusRepositoryTests
    {
        [TestMethod]
        public void LoadDropDownListSeriesStatus_GetAllStatusFromDb_ReturnStringList()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var expected = new List<String>();

            // Act
            expected.Add("In Progress");
            expected.Add("Awaiting Launch");
            expected.Add("No Started");
            expected.Add("Abandoned");
            expected.Add("Concluded");

            // Assert
            var result = service.loadDropDownListSeriesStatus();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LoadGridSeriesStatus_GetAllStatusFromDb_ReturnValidDataTable()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var expected = new DataTable();
            // Act
            var sent = service.LoadGridSeriesStatus();
            expected.Clear();
            expected.Columns.Add("Id");
            expected.Columns.Add("Description");

            DataRow row1 = expected.NewRow();
            row1["Id"] = 1;
            row1["Description"] = "In Progress";
            expected.Rows.Add(row1);

            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Description"] = "Awaiting Launch";
            expected.Rows.Add(row2);

            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["Description"] = "No Started";
            expected.Rows.Add(row3);

            DataRow row4 = expected.NewRow();
            row4["Id"] = 4;
            row4["Description"] = "Abandoned";
            expected.Rows.Add(row4);

            DataRow row5 = expected.NewRow();
            row5["Id"] = 5;
            row5["Description"] = "Concluded";
            expected.Rows.Add(row5);

            // Assert
            Assert.IsTrue(Helper.CompareDataTables(sent, expected));
        }

        [TestMethod]
        public void AddNewSeriesStatus_StatusIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var model = new BookSeriesStatu();
            // Act
            model.Description = "";
            var result = service.AddNewBookSerieStatusValidation(model);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddeNewSeriesStatus_StatusAlreadyExist_ReturnFalse()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var model = new BookSeriesStatu();
            // Act
            model.Description = "In Progress";
            var result = service.AddNewBookSerieStatusValidation(model);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddNewSeriesStatus_NewStatusToBeAdded_ReturnTrue()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var totalBeforeAdd = service.Count();
            var model = new BookSeriesStatu();

            // Act
            model.Description = "Test"; ;
            var result = service.AddNewBookSerieStatusValidation(model);
            var totalAfterAdded = service.Count();
            bool validation;

            // Assert
            if (result == true)
            {
                if (totalBeforeAdd < totalAfterAdded)
                {
                    validation = true;
                }
                else
                {
                    validation = false;
                }
            }
            else
            {
                validation = false;
            }

            Assert.IsTrue(validation);
        }

        [TestMethod]
        public void DeleteSeriesStatus_StatusIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var model = new BookSeriesStatu();

            // Act
            model.Description = "";
            var result = service.DeleteBookSerieStatusValidation(model.Description);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteSeriesStatus_StatusDoesNotExist_ReturnFalse()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var model = new BookSeriesStatu();
            // Act
            model.Description = "blablabla";
            var result = service.DeleteBookSerieStatusValidation(model.Description);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteSeriesStatus_StatusExist_ReturnTrue()
        {
            // Arrange
            var service = new BookSeriesStatusService();
            var model = new BookSeriesStatu();
            // Act
            model.Description = "Test";
            var result = service.DeleteBookSerieStatusValidation(model.Description);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
