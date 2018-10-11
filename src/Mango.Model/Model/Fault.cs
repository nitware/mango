using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Fault
    {
        public Fault() {}
                
        public Fault(string message)
        {
            Message = message;
        }

        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string Advice { get; set; }
        [DataMember]
        public string Action { get; set; }
    }



}
