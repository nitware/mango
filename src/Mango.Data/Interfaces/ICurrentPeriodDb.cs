using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface ICurrentPeriodDb
    {
        string FIELD_PERIOD_ID { get; }

        bool InsertCurrentPeriod(int periodId, Transaction transaction);
        bool DeleteAllCurrentPeriod(Transaction transaction);
        DataSet SelectAllCurrentPeriod();
                
    }


}
