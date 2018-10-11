using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using Mango.Model.Model;

namespace Mango.Model
{
    [DataContract]
    public class MetricRating
    {
        [DataMember]
        public Metrics Metrics { get; set; }
        [DataMember]
        public Rating Rating { get; set; }
        [DataMember]
        public decimal From { get; set; }
        [DataMember]
        public decimal To { get; set; }
        [DataMember]
        public RatingType RatingType { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }



}