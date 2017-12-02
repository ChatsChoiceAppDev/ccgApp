using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG.Contracts
{
  [DataContract]
  public class Comment
  {
    [DataMember]
    public string Text;

    [DataMember]
    public string UserID;

    [DataMember]
    public DateTime TimeStamp;
  }
}
