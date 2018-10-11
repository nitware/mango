using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Pace : ModelBase
    {
        private byte grade;
        private decimal score;
        private decimal weight;
        private string justification;
        private string rating;

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PaceAreaId { get; set; }
        [DataMember]
        public int AppraisalHeaderId { get; set; }
        [DataMember]
        public int PeriodId { get; set; }
        [DataMember]
        public string StaffId { get; set; }
        //[DataMember]
        //public decimal Weight { get; set; }
        //[DataMember]
        //public decimal Score { get; set; }
        //[DataMember]
        //public byte Grade { get; set; }
        //[DataMember]
        //public string Justification { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsSupervisor { get; set; }

        [DataMember]
        public decimal Weight        
        {
            get { return weight; }
            set
            {
                weight = value;
                base.OnPropertyChanged("Weight");
            }
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
        public byte Grade 
        {
            get { return grade; }
            set
            {
                grade = value;
                base.OnPropertyChanged("Grade");
            } 
        }
        [DataMember]
        public string Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                base.OnPropertyChanged("Rating");
            }
        }
        [DataMember]
        public string Justification 
        {
            get { return justification; }
            set
            {
                justification = value;
                base.OnPropertyChanged("Justification");
            }  
        }

       



    }
}