using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IPeriodDb
    {
        string FIELD_PERIOD_ID { get; }
        string FIELD_PERIOD_NAME { get; }
        string FIELD_STATUS_ID { get; }
        string FIELD_STATUS_NAME { get; }
        string FIELD_PERIOD_TYPE_ID { get; }
        string FIELD_SPAN { get; }
        string FIELD_START_DATE { get; }
        string FIELD_END_DATE { get; }

        int InsertPeriod(string periodName, byte statusId, int periodTypeId, byte span, DateTime startDate, DateTime endDate, int year, Transaction transaction);
        bool UpdatePeriodByPeriodId(int periodId, string periodName, byte statusId, string type, byte span, DateTime startDate, DateTime endDate, Transaction transaction);
        DataSet SelectPeriodByPeriodId(int periodId);
        DataSet SelectAllPeriod();


    }


}
