using CCG.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CCG.WebApi.Controllers
{
  public class SuggestionController : ApiController
  {
    //GET: api/Suggestion
    public IEnumerable<Option> Get()
    {
      List<Option> retList = new List<Option>();
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Suggestions;", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
          Option option = new Option();
          option.ID = (int)reader[0];
          option.Name = (string)reader[1];
          option.Description = (string)reader[2];
          retList.Add(option);
        }
      }

      return retList;
    }

    // POST: api/Suggestion
    public int Post(string option)
    {
      Option newOption = JsonConvert.DeserializeObject<Option>(option);
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO OptionSuggestions (Name, UserID, Description) OUTPUT Inserted.ID VALUES (@Name, @UserID, @Description);", conn);
        cmd.Parameters.AddWithValue("Name", newOption.Name);
        cmd.Parameters.AddWithValue("UserID", newOption.UserID);
        cmd.Parameters.AddWithValue("Description", newOption.Description);

        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
          int id = (int)reader[0];
          return id;
        }
      }
      return -1;
    }

    // PUT: api/Suggestion
    public void Put(string option)
    {
      Option updatadedOption = JsonConvert.DeserializeObject<Option>(option);
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("UPDATE OptionSuggestions SET Name = @Name, Description = @Description WERE UserID = @UserID", conn);
        cmd.Parameters.AddWithValue("Name", updatadedOption.Name);
        cmd.Parameters.AddWithValue("Description", updatadedOption.Description);
        cmd.Parameters.AddWithValue("UserID", updatadedOption.ID);

        cmd.ExecuteNonQuery();
      }
    }
  }
}
