using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTool
{
  public class StreamInfo
  {
    public StreamInfo()
    {
      Recurrence = -1;
    }

    public StreamInfo(StreamInfo streamInfo)
    {
      ID = streamInfo.ID;
      StreamName = streamInfo.StreamName;
      Recurrence = streamInfo.Recurrence;
      RecurrenceTimeType = streamInfo.RecurrenceTimeType;
      StreamDescription = streamInfo.StreamDescription;
      StartDateTime = streamInfo.StartDateTime;
      PlannedGames = streamInfo.PlannedGames;
    }
    public int ID = -1;
    public string StreamName
    {
      get; set;
    }
    public int Recurrence;
    public ReccurenceTimeTypes RecurrenceTimeType;
    public string StreamDescription;
    public DateTime StartDateTime;
    private List<string> m_plannedGames;
    public List<string> PlannedGames
    {
      get
      {
        if(m_plannedGames == null)
        {
          m_plannedGames = new List<string>();
        }

        return m_plannedGames;
      }

      set
      {
        m_plannedGames = value;
      }
    }
  }
}
