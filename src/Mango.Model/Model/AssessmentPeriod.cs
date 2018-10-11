using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    public class AssessmentPeriod
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Assessment Assessment { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }





}
