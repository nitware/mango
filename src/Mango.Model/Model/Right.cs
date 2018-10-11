using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Right : SetupBase
    {
        [DataMember]
        public bool IsInRole { get; set; }
    }


}
