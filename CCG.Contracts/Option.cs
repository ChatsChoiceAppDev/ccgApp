﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG.Contracts
{
  [DataContract]
  public class Option
  {
    [DataMember]
    public string Name;

    [DataMember]
    public string ID;

    [DataMember]
    public string Description;

    [DataMember]
    public double VoteValuePledged;

    [DataMember]
    public double VoteValueLocked;

    [DataMember]
    public string VoteValueType;

    [DataMember]
    Dictionary<string, object> DataSet;
  }
}
