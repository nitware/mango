using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    public class StaffAssessment : ModelBase
    {
        private short score;

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public AssessmentPeriod Period { get; set; }
        [DataMember]
        public Appraisal Appraisal { get; set; }

        //[DataMember]
        //public short Score { get; set; }

        [DataMember]
        public bool Enable { get; set; }

        [DataMember]
        public short Score
        {
            get { return score; }
            set
            {
                score = value;
                base.OnPropertyChanged("Score");
            }
        }
    }


}
