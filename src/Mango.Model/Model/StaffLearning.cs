using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class StaffLearning
    {
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public Period Period { get; set; }
        [DataMember]
        public decimal TrainingScore { get; set; }
        [DataMember]
        public decimal PercentScore { get; set; }
    }


}
