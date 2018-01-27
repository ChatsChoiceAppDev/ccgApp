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
    public int ID;

    [DataMember]
    public string Description;

    [DataMember]
    public string PollType;

    [DataMember]
    public bool IsActive;

    [DataMember]
    public DateTime EndTime;

    /// <summary>
    /// If the Poll has closed, the winning options will be contained in this
    /// array.
    /// </summary>
    [DataMember]
    public Option[] WinningOptions;

    [DataMember]
    public bool IsVotingRestricted;

    [DataMember]
    public List<Comment> Comments;

    [DataMember]
    public List<Option> Options;
  }
}
