using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class Inps : ModelBase
    {
        private decimal? score;
        private decimal rating;
               
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public InpsType Type { get; set; }
       
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public string Kpi { get; set; }
        [DataMember]
        public string Measure { get; set; }
        [DataMember]
        public string DataSource { get; set; }
        [DataMember]
        public Department ResponsibleDepartment { get; set; }
       
        [DataMember]
        public decimal? Target { get; set; }

        [DataMember]
        public Period Period { get; set; }
       
        [DataMember]
        public bool IsSupervisor { get; set; }
              
        [DataMember]
        public decimal TotalScore { get; set; }

        [DataMember]
        public List<MetricRating> MetricRatings { get; set; }
        
        [DataMember]
        public decimal? Score
        {
            get { return score; }
            set
            {
                score = value;
                base.OnPropertyChanged("Score");
            }
        }

        [DataMember]
        public decimal Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                base.OnPropertyChanged("Rating");
            }
        }




    }
}
