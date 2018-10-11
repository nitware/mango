using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Caterer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Alias { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int TotalScore { get; set; }
        [DataMember]
        public decimal AggregateScore { get; set; }
        [DataMember]
        public List<ScoreTable> ScoreTables { get; set; }
    }



}
