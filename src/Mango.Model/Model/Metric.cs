using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public abstract class Metric : ModelBase
    {
        private decimal score;
        private decimal rating;
        private List<MetricRating> metricRatings;
        //private StaffMetric staffMetric;

        public Metric()
        {
            metricRatings = new List<MetricRating>();
            //staffMetric = new StaffMetric();
        }
               
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int PerspectiveId { get; set; }
        [DataMember]
        public string Perspective { get; set; }
        [DataMember]
        public byte Percentage { get; set; }
        [DataMember]
        public string Kpi { get; set; }
        [DataMember]
        public string Measure { get; set; }
        [DataMember]
        public bool IsSupervisor { get; set; }
        [DataMember]
        public decimal Target { get; set; }
        [DataMember]
        public Period Period { get; set; }
        [DataMember]
        public int CompanyDepartmentJobRoleId { get; set; }
        [DataMember]
        public string DataSource { get; set; }
        [DataMember]
        public string ResponsibilityDepartmentId { get; set; }

        
        [DataMember]
        public int StaffMetricId { get; set; }
        [DataMember]
        public decimal TotalScore { get; set; }

        [DataMember]
        public List<MetricRating> MetricRatings
        {
            get { return metricRatings; }
            set { metricRatings = value; }
        }

        [DataMember]
        public decimal Score
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