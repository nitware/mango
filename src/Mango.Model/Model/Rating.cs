using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Rating
    {
        //[DataMember]
        //public int Id { get; set; }
        //[DataMember]
        //public string Name { get; set; }
        //[DataMember]
        //public int Grade { get; set; }

        [DataMember]
        public byte Id { get; set; }
        [DataMember]
        public byte Name { get; set; }
       
    }


}
