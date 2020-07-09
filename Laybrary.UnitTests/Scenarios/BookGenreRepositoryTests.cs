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
    public class BookGenreRepositoryTests
    {
        [TestMethod]
        public void LoadDropDownListGenre_returnStringList()
        {
            // Arrange
            var service = new BookGenreService();

            // Act
            List<String> expected = new List<String>();
            expected.Add("Fiction");
            expected.Add("Non-Fiction");           

            // Assert
            var result = service.LoadDropDownListGenres();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LoadGridGenre_returnValidDataTable()
        {
            // Arrange
            var service = new BookGenreService();

            // Act
            var sent = service.LoadGridGenres();
            var expected = new DataTable();
            expected.Clear();
            expected.Columns.Add("Id");
            expected.Columns.Add("Description");

            DataRow row1 = expected.NewRow();
            row1["Id"] = 1;
            row1["Description"] = "Fiction";
            expected.Rows.Add(row1);

            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Description"] = "Non-Fiction";
            expected.Rows.Add(row2);

            Assert.IsTrue(Helper.CompareDataTables(sent, expected));
        }

        [TestMethod]
        public void AddNewGenre_NewGenreIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookGenreService();

            // Act
            var model = new BookGenre();
            model.Description = "";

            var result = service.AddNewBookGenreValidation(model);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddNewGenre_NewGenreSentAlreadyExist_ReturnFalse()
        {
            // Arrange
            var service = new BookGenreService();

            // Act
            var model = new BookGenre();
            model.Description = "Fiction";

            var result = service.AddNewBookGenreValidation(model);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddNewGenre_NewGenreToBeAdded_ReturnOneMoreToBeCount()
        {
            // Arrange
            var service = new BookGenreService();
            var totalBeforeAdd = service.Count();

            // Act
            var model = new BookGenre();
            model.Description = "Test";

            var result = service.AddNewBookGenreValidation(model);
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
        public void DeleteGenre_GenreIsEmpty_ReturnFalse()
        {
            // Arrange
            var service = new BookGenreService();
            var model = new BookGenre();
            // Act
            model.Description = "";
            var result = service.DeleteBookGenreValidation(model.Description);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteGenre_GenreDoesNotExist_ReturnFalse()
        {
            // Arrange
            var service = new BookGenreService();
            var model = new BookGenre();
            // Act
            model.Description = "blablablabla";
            var result = service.DeleteBookGenreValidation(model.Description);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteGenre_GenreDeleted_ReturnTrue()
        {
            // Arrange
            var service = new BookGenreService();
            var model = new BookGenre();
            // Act
            model.Description = "Test";
            var result = service.DeleteBookGenreValidation(model.Description);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
