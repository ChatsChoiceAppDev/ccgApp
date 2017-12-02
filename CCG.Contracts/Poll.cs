using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CCG.Contracts
{
  [DataContract]
  public class Poll
  {
    [DataMember]
    public string Name;

    [DataMember]
    public string ID;

    [DataMember]
    public string Description;

    [DataMember]
    public string PollType;

    [DataMember]
    public List<string> PollOptions;

    [DataMember]
    public bool IsActive;

    [DataMember]
    public DateTime EndTime;

    [DataMember]
    public double EndValue;

    [DataMember]
    public bool IsVotingRestricted;

    [DataMember]
    public List<Comment> Comments;
  }
}
