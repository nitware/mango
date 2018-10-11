using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Mango.Model
{
    [DataContract]
    public class Metrices
    {
        public Metrices()
        {
            Customers = new List<Customer>();
            Financials = new List<Financial>();
            Peoples = new List<People>();
            Processes = new List<Process>();
            Risks = new List<Risk>();
        }

        [DataMember]
        public decimal CustomerSumTotal { get; set; }
        [DataMember]
        public decimal FinancialSumTotal { get; set; }
        [DataMember]
        public decimal ProcessSumTotal { get; set; }
        [DataMember]
        public decimal PeopleSumTotal { get; set; }
        [DataMember]
        public decimal RiskSumTotal { get; set; }

        [DataMember]
        public byte Grade { get; set; }

        [DataMember]
        public decimal CustomerActualScoreTotal { get; set; }
        [DataMember]
        public decimal FinancialActualScoreTotal { get; set; }
        [DataMember]
        public decimal ProcessActualScoreTotal { get; set; }
        [DataMember]
        public decimal PeopleActualScoreTotal { get; set; }
        [DataMember]
        public decimal RiskActualScoreTotal { get; set; }

        [DataMember]
        public string CustomerTarget { get; set; }
        [DataMember]
        public string FinancialTarget { get; set; }
        [DataMember]
        public string ProcessTarget { get; set; }
        [DataMember]
        public string PeopleTarget { get; set; }
        [DataMember]
        public string RiskTarget { get; set; }

        [DataMember]
        public decimal CustomerTargetValue { get; set; }
        [DataMember]
        public decimal FinancialTargetValue { get; set; }
        [DataMember]
        public decimal ProcessTargetValue { get; set; }
        [DataMember]
        public decimal PeopleTargetValue { get; set; }
        [DataMember]
        public decimal RiskTargetValue { get; set; }
              
        [DataMember]
        public List<Customer> Customers { get; set; }
        [DataMember]
        public List<Financial> Financials { get; set; }
        [DataMember]
        public List<People> Peoples { get; set; }
        [DataMember]
        public List<Process> Processes { get; set; }
        [DataMember]
        public List<Risk> Risks { get; set; }
    }




}