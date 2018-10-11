using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class ScoreTable : ModelBase
    {
        private bool excellent;
        private bool veryGood;
        private bool good;
        private bool fair;
        private bool poor;

        [DataMember]
        public int CatererId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public int RatingCriteriaId { get; set; }
        [DataMember]
        public string RatingCriteriaName { get; set; }
       
        
        [DataMember]
        public bool Excellent
        {
            get { return excellent; }
            set
            {
                excellent = value;
                base.OnPropertyChanged("Excellent");
            }
        }
        [DataMember]
        public bool VeryGood
        {
            get { return veryGood; }
            set
            {
                veryGood = value;
                base.OnPropertyChanged("VeryGood");
            }
        }
        [DataMember]
        public bool Good
        {
            get { return good; }
            set
            {
                good = value;
                base.OnPropertyChanged("Good");
            }
        }
        [DataMember]
        public bool Fair
        {
            get { return fair; }
            set
            {
                fair = value;
                base.OnPropertyChanged("Fair");
            }
        }
        [DataMember]
        public bool Poor
        {
            get { return poor; }
            set
            {
                poor = value;
                base.OnPropertyChanged("Poor");
            }
        }

        [DataMember]
        public int Score { get; set; }



    }



}
