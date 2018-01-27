using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG.Contracts
{
  [DataContract]
  public class User
  {
    [DataMember]
    public int ID;

    [DataMember]
    public int TwitchID;

    [DataMember]
    public string Name;

    [DataMember]
    public int Experience;
  }
}
