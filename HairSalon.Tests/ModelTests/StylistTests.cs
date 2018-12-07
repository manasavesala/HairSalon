using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=manasa_vesala_test;";
    }

    public void Dispose()
    {
      Client.ClearAll();
      Stylist.ClearAll();
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("manasa", "seattle", "4.5 stars");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetStylistName_ReturnsName_String()
    {
      //Arrange
      string name = "manasa";
      string rating = "4.5 stars";
      string location = "seattle";
      Stylist newStylist = new Stylist(name, location , rating);

      //Act
      string result = newStylist.GetStylistName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsStylistId_Int()
    {
      //Arrange
      string name = "Test Category";
      string location = "seattle";
      string rating = "food";
      Stylist newStylist = new Stylist(name,location , rating);
    
      //Act
      newStylist.Save();
      int result = newStylist.GetId();
    
      //Assert
      Assert.AreEqual(newStylist.GetId(), result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllResataurantObjects_ResataurantList()
    {
      //Arrange
      string name = "Work";
      string location = "seattle";
      string rating = "food";
      string name2 = "School";
      string location2 = "seattle";
      string rating2 = "food";
      Stylist newStylist1 = new Stylist(name,location , rating);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name2,location2 , rating2);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      //Act
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("manasa", "portland","5 stars");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetStylist_ReturnsEmptyList_AfterDelete()
    {
      //Arrange
      string name = "manasa";
      string location = "seattle";
      string rating = "5 stars";
      Stylist newStylist = new Stylist(name, location, rating);
      newStylist.Save();
      Stylist.DeleteStylist(newStylist.GetId());

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> newList = new List<Stylist> {};

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetStylist_ReturnsUpdatedList_AfterEdit()
    {
      //Arrange
      string name = "manasa";
      string location = "seattle";
      string rating = "5 stars";
      string name2 = "deepica";
      string location2 = "portland";
      string rating2 = "4 stars";
      Stylist newStylist = new Stylist(name, location, rating);
      newStylist.Save();

      newStylist.Edit(name2, location2 , rating2);

      //Act
      string result = newStylist.GetStylistName();

      //Assert
     Assert.AreEqual(name2, result);
    }


    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("manasa", "Seattle", "5 stars");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("deepica", "Seattle", "Seafood");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}