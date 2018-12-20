using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
      private string _stylistName;
      private string _location;
      private string _rating;
      private int _id;

      public Stylist(string stylistName, string location, string rating, int id=0)
      {
        _stylistName = stylistName;
        _location = location;
        _rating = rating;
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
    public string GetLocation()
    {
      return _location;
    }
    public string GetRating()
    {
      return _rating;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (stylistName, location, rating) VALUES (@stylistName, @location, @rating);";
      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;
      cmd.Parameters.Add(stylistName);

      MySqlParameter location = new MySqlParameter();
      location.ParameterName = "@location";
      location.Value = this._location;
      cmd.Parameters.Add(location);

      MySqlParameter rating = new MySqlParameter();
      rating.ParameterName = "@rating";
      rating.Value = this._rating;
      cmd.Parameters.Add(rating);      

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string StylistName = rdr.GetString(0);
        string location = rdr.GetString(1);
        string rating = rdr.GetString(2);
        int StylistId = rdr.GetInt32(3);
        Stylist newStylist = new Stylist( StylistName, location, rating,StylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }  

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int StylistId = 0;
      string StylistName = "";
      string location = "";
      string rating = "";
      while(rdr.Read())
      {
        StylistName = rdr.GetString(0);
        location = rdr.GetString(1);
        rating = rdr.GetString(2);
        StylistId = rdr.GetInt32(3);
      }
      Stylist newStylist = new Stylist(StylistName, location, rating,StylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

    public List<Client> GetClients()
    {
      List<Client> allStylistsClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylistName = @stylistName;";
      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;
      cmd.Parameters.Add(stylistName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string ClientName = rdr.GetString(0);
        string stylistName1 = rdr.GetString(1);
        int ClientId = rdr.GetInt32(2);
        Client newClient = new Client(ClientName, stylistName1);
        allStylistsClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylistsClients;
    } 

     public static void DeleteStylist(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = (@thisId);";
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

    public void Edit(string newStylistName, string newlocation, string newrating)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET stylistName = @newStylistName, location = @newlocation, rating = @newrating WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@newStylistName";
      stylistName.Value = newStylistName;
      cmd.Parameters.Add(stylistName);

      MySqlParameter location = new MySqlParameter();
      location.ParameterName = "@newlocation";
      location.Value = newlocation;
      cmd.Parameters.Add(location);

      MySqlParameter rating = new MySqlParameter();
      rating.ParameterName = "@newrating";
      rating.Value = newrating;
      cmd.Parameters.Add(rating);

      cmd.ExecuteNonQuery();
      _stylistName = newStylistName;
      _location = newlocation; 
      _rating = newrating;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    } 

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId().Equals(newStylist.GetId());
        bool StylistNameEquality = this.GetStylistName().Equals(newStylist.GetStylistName());
        bool StylistLocationEquality = this.GetLocation().Equals(newStylist.GetLocation());
        bool StylistRatingEquality = this.GetRating().Equals(newStylist.GetRating());
        return (idEquality && StylistNameEquality && StylistLocationEquality && StylistRatingEquality);
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists; DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }    

    public void AddSpecialty(int specialtyId, int stylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (specialty_id, stylist_id) VALUES (@specialtyId, @stylistId);";
      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@specialtyId";
      specialty_id.Value = specialtyId;
      cmd.Parameters.Add(specialty_id);
      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@stylistId";
      stylist_id.Value = stylistId;
      cmd.Parameters.Add(stylist_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public List<Specialty> FindSpecialtiesOfStylist(int stylistId)
    {
      List<Specialty> stylistSpecialties = new List<Specialty> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialties.* FROM
          specialties JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
          WHERE stylists_specialties.stylist_id =" + stylistId + ";";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
          int SpecialtyId = rdr.GetInt32(0);
          string name = rdr.GetString(1);
          Specialty stylistSpecialty = new Specialty(name, SpecialtyId);
          stylistSpecialties.Add(stylistSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return stylistSpecialties;
    }

    public static List<Stylist> FindStylistsOfStylist(int specialtyId)
    {
      List<Stylist> specalityStylists = new List<Stylist> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM
          stylists JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
          WHERE stylists_specialties.specialty_id =" + specialtyId + ";";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
          int id = rdr.GetInt32(3);
          string stylistName = rdr.GetString(0);
          string location = rdr.GetString(1);
          string rating = rdr.GetString(2);
          Stylist stylistStylist = new Stylist(stylistName,location,rating,id);
          specalityStylists.Add(stylistStylist);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return specalityStylists;
    }
  }
}
