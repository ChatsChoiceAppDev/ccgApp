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
  public class UserController : ApiController
  {
    // GET: api/User
    public IEnumerable<User> Get()
    {
      List<User> retList = new List<User>();
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users;", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
          User user = new User();
          user.ID = (int)reader[0];
          user.Name = (string)reader[1];
          user.TwitchID = (int)reader[2];
          user.Experience = (int)reader[3];
          retList.Add(user);
        }
      }

      return retList;
    }

    // GET: api/User/5
    public User Get(int id)
    {
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE ID=@ID;", conn);
        cmd.Parameters.AddWithValue("ID", id);
        SqlDataReader reader = cmd.ExecuteReader();

        User user = new User();
        if (reader.Read())
        {
          user.ID = (int)reader[0];
          user.Name = (string)reader[1];
          user.TwitchID = (int)reader[2];
          user.Experience = (int)reader[3];
        }

        return user;
      }
    }

    [Route("api/user/TwitchID/{twitchID}")]
    public User GetByTwitchID(int twitchID)
    {
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE TwitchID=@ID;", conn);
        cmd.Parameters.AddWithValue("ID", twitchID);
        SqlDataReader reader = cmd.ExecuteReader();

        User user = new User();
        if (reader.Read())
        {
          user.ID = (int)reader[0];
          user.Name = (string)reader[1];
          user.TwitchID = (int)reader[2];
          user.Experience = (int)reader[3];
        }

        return user;
      }
    }

    // POST: api/User
    public int Post(string user)
    {
      User newUser = JsonConvert.DeserializeObject<User>(user); 
      using (SqlConnection conn = new SqlConnection(Util.ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO Users (TwitchID, Name, Experience) OUTPUT Inserted.ID VALUES (@TwitchID, @Name, @Exp);", conn);
        cmd.Parameters.AddWithValue("TwitchID", newUser.TwitchID);
        cmd.Parameters.AddWithValue("Name", newUser.Name);
        cmd.Parameters.AddWithValue("Exp", 0);

        SqlDataReader reader = cmd.ExecuteReader();
        if(reader.Read())
        {
          int id = (int)reader[0];
          return id;
        }
      }
      return -1;
    }

    // PUT: api/User/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE: api/User/5
    public void Delete(int id)
    {
    }
  }
}
