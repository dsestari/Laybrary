
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
    public class BookSourceRepositoryTests
    {
        [TestMethod]
        public void LoadDropDownListSource_returnStringList()
        {
            // Arrange
            var service = new BookSourceService();

            // Act
            List<String> expected = new List<String>();
            expected.Add("Collection");
            expected.Add("Ebook");
            expected.Add("Library");           

            // Assert
            var result = service.LoadDropDownListSource();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LoadGridSource_returnValidDataTable()
        {
            // Arrange
            var service = new BookSourceService();

            // Act
            var sent = service.LoadGridSource();
            var expected = new DataTable();
            expected.Clear();
            expected.Columns.Add("Id");
            expected.Columns.Add("Description");

            DataRow row1 = expected.NewRow();
            row1["Id"] = 1;
            row1["Description"] = "Collection";
            expected.Rows.Add(row1);

            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Description"] = "Ebook";
            expected.Rows.Add(row2);

            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["Description"] = "Library";
            expected.Rows.Add(row3);           

            Assert.IsTrue(Helper.CompareDataTables(sent, expected));
        }

        [TestMethod]
        public void AddNewSource_NewSourceIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookSourceService();

            // Act
            var model = new BookSource();
            model.Description = "";

            var result = service.AddNewBookSourcesValidation(model);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddNewSource_NewSourceSentAlreadyExist_ReturnFalse()
        {
            // Arrange
            var service = new BookSourceService();

            // Act
            var model = new BookSource();
            model.Description = "Collection";

            var result = service.AddNewBookSourcesValidation(model);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddNewSource_NewSourceToBeAdded_ReturnOneMoreToBeCount()
        {
            // Arrange
            var service = new BookSourceService();
            var totalBeforeAdd = service.Count();

            // Act
            var model = new BookSource();
            model.Description = "Test";

            var result = service.AddNewBookSourcesValidation(model);
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
        public void DeleteSource_SourceIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookSourceService();
            var model = new BookSource();
            // Act
            model.Description = "";
            var result = service.DeleteBookSourcesValidation(model.Description);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteSource_SourceDoesNotExist_ReturnFalse()
        {
            // Arrange
            var service = new BookSourceService();
            var model = new BookSource();
            // Act
            model.Description = "blablablabla";
            var result = service.DeleteBookSourcesValidation(model.Description);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteSource_SourceDeleted_ReturnTrue()
        {
            // Arrange
            var service = new BookSourceService();
            var model = new BookSource();
            // Act
            model.Description = "Test";
            var result = service.DeleteBookSourcesValidation(model.Description);
            // Assert
            Assert.IsTrue(result);
        }

    }
}
