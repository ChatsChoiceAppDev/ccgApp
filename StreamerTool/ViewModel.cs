using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTool
{
  public enum ReccurenceTimeTypes
  {
    Hours,
    Days,
    Weeks,
    Months,
    None
  }

  public sealed class ViewModel
  {
    private const string ConnectString = @"Server=localhost\SQLEXPRESS;Database=CCG;Trusted_Connection=True;";

    ViewModel()
    {
    }

    private static readonly object padlock = new object();

    private static ViewModel m_instance;
    public static ViewModel Instance
    {
      get
      {
        lock(padlock)
        {
          if (m_instance == null)
          {
            m_instance = new ViewModel();
          }

          return m_instance;
        }
      }
    }

    public int AddStream(string streamTitle, DateTime startDateTime, 
      List<string> plannedGames = null, string streamDescription = "")
    {
      //Add this stream to the Streams SQL table
      return 0;
    }

    public int AddStream(StreamInfo streamInfo)
    {
      //Add this stream to the Streams SQL table
      return 0;
    }

    public List<StreamInfo> LoadStreams()
    {
      using (SqlConnection conn = new SqlConnection(ConnectString))
      {
        List<StreamInfo> retVal = new List<StreamInfo>();

        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Streams;", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
          StreamInfo stream = new StreamInfo();
          stream.StreamName = (string)reader[0];
          stream.StreamDescription = (string)reader[1];
          stream.StartDateTime = (DateTime)reader[2];
          stream.ID = (int)reader[3];
          if (reader[4].GetType() != typeof(DBNull))
          {
            stream.Recurrence = (int)reader[4];
          }
          if (reader[6].GetType() != typeof(DBNull))
          {
            byte value = (byte)reader[6];
            int val32 = Convert.ToInt32(value);
            
            stream.RecurrenceTimeType = (ReccurenceTimeTypes)val32;
          }
          retVal.Add(stream);
        }

        return retVal;
      }
    }
  }
}
