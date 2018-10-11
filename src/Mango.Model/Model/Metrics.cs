using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class Metrics
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public MetricsPerspective Perspective { get; set; }
        [DataMember]
        public CompanyDepartmentJobRole CompanyDepartmentJobRole { get; set; }
        [DataMember]
        public string JobRoleName { get; set; }
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
        public decimal? Score { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }





}
