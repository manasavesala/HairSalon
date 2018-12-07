using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=manasa_vesala_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("manasa","deepica");
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "manasa";
      Client newClient = new Client(name , "Deepica");

      //Act
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsClientId_Int()
    {
      //Arrange
      string name = "manasa";
      string stylistName = "sindhu";
      Client newClient = new Client(name, stylistName);
    
      //Act
      newClient.Save();
      int result = newClient.GetId();
    
      //Assert
      Assert.AreEqual(newClient.GetId(), result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllClientObjects_ClientList()
    {
      //Arrange
      string name = "manasa";
      string stylistName = "deepica";
      string stylistName2 = "swathi";
      string name2 = "deepica";

      Client newClient1 = new Client(name, stylistName);
      newClient1.Save();
      Client newClient2 = new Client(name2, stylistName2);
      newClient2.Save();
      List<Client> newList = new List<Client> { newClient1, newClient2 };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetClient_ReturnsEmptyList_AfterDelete()
    {
      //Arrange
      string name = "manasa";
      string stylistName = "shilpa";
      Client newClient = new Client(name, stylistName);
      newClient.Save();
      Client.DeleteClient(newClient.GetId());

      //Act
      List<Client> result = Client.GetAll();
      List<Client> newList = new List<Client> {};

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }


    [TestMethod]
    public void Save_SavesClientToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("manasa", "sapna");
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToClient_Id()
    {
      //Arrange
      Client testClient = new Client("ajay","shilpa");
      testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}