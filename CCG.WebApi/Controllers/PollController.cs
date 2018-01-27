using CCG.Contracts;
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;

namespace CCG.WebApi.Controllers
{
  public class PollController : ApiController
  {
    private const string ConnectString = @"Server=localhost\SQLEXPRESS;Database=CCG;Trusted_Connection=True;";
    private readonly string m_pw = "Brinter3.1415";

    // GET: api/Poll
    public IEnumerable<Poll> Get()
    {
      List<Poll> retList = new List<Poll>();
      var pw = Util.ConvertToSecureString("Brinter3.1415");
      SqlCredential credential = new SqlCredential("Ronaldo", pw);
      using (SqlConnection conn = new SqlConnection(ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Polls;", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
          Poll poll = new Poll();
          poll.ID = (int)reader[0];
          poll.Name = (string)reader[1];
          poll.Description = (string)reader[2];
          poll.PollType = (string)reader[3];
          poll.IsActive = (bool)reader[4];
          poll.EndTime = (DateTime)reader[5];
          poll.IsVotingRestricted = (bool)reader[6];
          retList.Add(poll);
        }
      }

      return retList;
    }

    // GET: api/Poll/5
    public Poll Get(int id)
    {
      using (SqlConnection conn = new SqlConnection(ConnectString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand($"SELECT * FROM Polls WHERE ID=@ID;", conn);
        cmd.Parameters.AddWithValue("ID", id);
        SqlDataReader reader = cmd.ExecuteReader();

        Poll poll = new Poll();

        if (reader.Read())
        {
          poll.ID = (int)reader[0];
          poll.Name = (string)reader[1];
          poll.Description = (string)reader[2];
          poll.PollType = (string)reader[3];
          poll.IsActive = (bool)reader[4];
          poll.EndTime = (DateTime)reader[5];
          poll.IsVotingRestricted = (bool)reader[6];
        }

        return poll;
      }
    }

    // POST: api/Poll
    public void Post([FromBody]Poll value)
    {
    }

    // PUT: api/Poll/5
    public void Put(int id, [FromBody]Poll value)
    {
    }

    // DELETE: api/Poll/5
    public void Delete(int id)
    {
    }
  }
}
