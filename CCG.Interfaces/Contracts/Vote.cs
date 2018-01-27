using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG.Contracts
{
  [DataContract]
  public class Vote
  {
    [DataMember]
    public string ID;

    [DataMember]
    public double Value;

    [DataMember]
    public string UserID;

    [DataMember]
    public string OptionID;

    [DataMember]
    public bool IsLocked;
  }
}
