using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class CurrentPeriod
    {
        [DataMember]
        public Period Period { get; set; }
    }


}
