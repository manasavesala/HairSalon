using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class SpecialtyTest : IDisposable
  {
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=manasa_vesala_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Specialty.ClearAll();
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("Hair Stylist");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Hair Stylist";
      Specialty newSpecialty = new Specialty(name);

      //Act
      string result = newSpecialty.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsSpecialtyId_Int()
    {
      //Arrange
      string name = "manasa";
      Specialty newSpecialty = new Specialty(name);
    
      //Act
      newSpecialty.Save();
      int result = newSpecialty.GetId();
    
      //Assert
      Assert.AreEqual(newSpecialty.GetId(), result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
    {
      //Arrange
      string name = "manasa";
      string name2 = "deepica";

      Specialty newSpecialty1 = new Specialty(name);
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty(name2);
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

      //Act
      List<Specialty> result = Specialty.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetSpecialty_ReturnsEmptyList_AfterDelete()
    {
      //Arrange
      string name = "Hair Stylist";
      Specialty newSpecialty = new Specialty(name);
      newSpecialty.Save();
      Specialty.DeleteSpecialty(newSpecialty.GetId());

      //Act
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> newList = new List<Specialty> {};

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }


    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Hair Stylist");
      testSpecialty.Save();

      //Act
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToSpecialty_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Hair Stylist");
      testSpecialty.Save();

      //Act
      Specialty savedSpecialty = Specialty.GetAll()[0];

      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

  }
}