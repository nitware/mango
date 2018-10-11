using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IPaceAreaDb
    {
        string FIELD_PACE_AREA_ID { get; }
        string FIELD_PACE_NAME { get; }
        string FIELD_PACE_DESCRIPTION { get; }

        bool InsertPaceArea(int paceAreaId, string paceName, string paceDescription, Transaction transaction);
        bool DeletePaceAreaByPaceAreaId(int paceAreaId, Transaction transaction);
        bool UpdatePaceAreaByPaceAreaId(int paceAreaId, string paceName, string paceDescription, Transaction transaction);
        DataSet SelectAllPaceArea();
        DataSet SelectPaceAreaByPaceAreaId(int paceAreaId);
       

    }
}
