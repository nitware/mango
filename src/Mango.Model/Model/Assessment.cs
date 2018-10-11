using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    public class Assessment
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Skill { get; set; }
        [DataMember]
        public string Indicator { get; set; }
    }




}
