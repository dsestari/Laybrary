using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laybrary.UnitTests.LaybraryServices;
using System.Collections.Generic;
using System.Data;
using Laybrary.Useful;
using Laybrary.UnitTests.AppDataTest;

namespace Laybrary.UnitTests.Scenarios
{
    [TestClass]
    public class BookStatusRepositoryTests
    {
        //[TestMethod]
        //public void LoadDropDownListStatus_returnStringList()
        //{
        //    // Arrange
        //    var service = new BookStatusService(); 

        //    // Act
        //    List<String> expected = new List<String>();
        //    expected.Add("TBR");
        //    expected.Add("Read");
        //    expected.Add("Deleted");
        //    expected.Add("Collectable");

        //    // Assert
        //    var result = service.LoadDropDownListStatus();
        //    CollectionAssert.AreEqual(expected, result);            
        //}

        //[TestMethod]
        //public void LoadGridStatus_returnValidDataTable()
        //{
        //    // Arrange
        //    var service = new BookStatusService();

        //    // Act
        //    var sent = service.LoadGridStatus();
        //    var expected = new DataTable();
        //    expected.Clear();
        //    expected.Columns.Add("Id");
        //    expected.Columns.Add("Status");

        //    DataRow row1 = expected.NewRow();
        //    row1["Id"] = 1;
        //    row1["Status"] = "TBR";
        //    expected.Rows.Add(row1);

        //    DataRow row2 = expected.NewRow();
        //    row2["Id"] = 2;
        //    row2["Status"] = "Read";
        //    expected.Rows.Add(row2);

        //    DataRow row3 = expected.NewRow();
        //    row3["Id"] = 3;
        //    row3["Status"] = "Deleted";
        //    expected.Rows.Add(row3);

        //    DataRow row4 = expected.NewRow();
        //    row4["Id"] = 4;
        //    row4["Status"] = "Collectable";
        //    expected.Rows.Add(row4);

        //    Assert.IsTrue(Helper.CompareDataTables(sent, expected));
        //}

        //[TestMethod]
        //public void AddNewStatus_NewStatusIsEmpty_ReturnFalse()
        //{
        //    // Arrange
        //    var service = new BookStatusService();

        //    // Act
        //    var model = new BookStatu();
        //    model.Status = "";

        //    var result = service.AddNewBookStatusValidation(model);

        //    // Assert
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AddNewStatus_NewStatusSentAlreadyExist_ReturnFalse()
        //{
        //    // Arrange
        //    var service = new BookStatusService();

        //    // Act
        //    var model = new BookStatu();
        //    model.Status = "TBR";

        //    var result = service.AddNewBookStatusValidation(model);

        //    // Assert
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AddNewStatus_NewStatusToBeAdded_ReturnOneMoreToBeCount()
        //{
        //    // Arrange
        //    var service = new BookStatusService();
        //    var totalBeforeAdd = service.Count();

        //    // Act
        //    var model = new BookStatu();
        //    model.Status = "Test";

        //    var result = service.AddNewBookStatusValidation(model);
        //    var totalAfterAdded = service.Count();
        //    bool validation;
                  
        //    // Assert
        //    if (result == true)
        //    {
        //        if (totalBeforeAdd < totalAfterAdded)
        //        {
        //            validation = true;
        //        }
        //        else
        //        {
        //            validation = false;
        //        }

        //    }
        //    else
        //    {
        //        validation = false;
        //    }

        //    Assert.IsTrue(validation);
        //}

        //[TestMethod]
        //public void DeleteStatus_StatusIsEmpty_ReturnFalse()
        //{
        //    // Arrange
        //    var service = new BookStatusService();
        //    var model = new BookStatu();
        //    // Act
        //    model.Status = "";
        //    var result = service.DeleteBookStatusValidation(model.Status);
        //    // Assert
        //    Assert.IsFalse(result);
        //}

        [TestMethod]
        public void DeleteStatus_StatusDoesNotExist_ReturnFalse()
        {
            // Arrange
            var service = new BookStatusService();
            var model = new BookStatu();
            // Act
            model.Status = "blablablabla";
            var result = service.DeleteBookStatusValidation(model.Status);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
