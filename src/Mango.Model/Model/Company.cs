using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Runtime.Serialization;

namespace Mango.Model
{
    public class Company
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
    }
}