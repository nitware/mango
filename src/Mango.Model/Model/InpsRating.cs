using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class InpsRating
    {
        //[DataMember]
        //public long Id { get; set; }

        [DataMember]
        public Period Period { get; set; }
        [DataMember]
        public Rating Rating { get; set; }
        [DataMember]
        public decimal From { get; set; }
        [DataMember]
        public decimal To { get; set; }
        [DataMember]
        public RatingType RatingType { get; set; }
        [DataMember]
        public InpsType Type { get; set; }
       
    }



}
