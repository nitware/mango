using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    public class AssessmentLevel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Level Level { get; set; }
        [DataMember]
        public Period Period { get; set; }
       
    }



}
