using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialty
  {
    private string _name;
    private int _id;

    public Specialty( string name,int id=0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    
    public string GetName()
    {
      return _name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (name) VALUES ( @name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        int SpecialtyId = rdr.GetInt32(1);
        Specialty newSpecialty = new Specialty( name,SpecialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }  

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int SpecialtyId = 0;
      string name = "";
      while(rdr.Read())
      {
        name = rdr.GetString(1);
        SpecialtyId = rdr.GetInt32(0);
      }
      Specialty newSpecialty = new Specialty( name,SpecialtyId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    } 

     public static void DeleteSpecialty(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = (@thisId);";
      
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = this.GetId().Equals(newSpecialty.GetId());
        bool nameEquality = this.GetName().Equals(newSpecialty.GetName());
        return (idEquality && nameEquality);
      }
    }    

  }
}