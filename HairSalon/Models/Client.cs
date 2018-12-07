using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private string _phonenumber;
    private string _stylistName;
    private int _id;

    public Client( string name, string phonenumber,string stylistName, int id=0)
    {
      _name = name;
      _phonenumber = _phonenumber;
      _stylistName = stylistName;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetStylistName()
    {
      return _stylistName;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetPhonenumber()
    {
      return _phonenumber;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, phonenumber, stylistName) VALUES ( @name, @phonenumber, @stylistName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter phonenumber = new MySqlParameter();
      phonenumber.ParameterName = "@phonenumber";
      phonenumber.Value = this._phonenumber;
      cmd.Parameters.Add(phonenumber);    

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;
      cmd.Parameters.Add(stylistName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        string phonenumber = rdr.GetString(1);
        string StylistName = rdr.GetString(2);
        int ClientId = rdr.GetInt32(3);
        Client newClient = new Client( name, phonenumber, StylistName, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }  

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clints WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ClientId = 0;
      string name = "";
      string phonenumber = "";
      string StylistName = "";
      while(rdr.Read())
      {
        name = rdr.GetString(0);
        phonenumber = rdr.GetString(1);
        StylistName = rdr.GetString(2);
        ClientId = rdr.GetInt32(3);
      }
      Client newClient = new Client( name, phonenumber,StylistName,ClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newClient;
    }  
  }
}