using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Mango.Data.Interfaces
{
    public interface ILearningDb
    {
        string FIELD_STAFF_ID { get; }
        string FIELD_PERIOD_ID { get; }
        string FIELD_TRAINING_SCORE { get; }
        string FIELD_PERCENTAGE_SCORE { get; }

        DataSet SelectStaffLearningByStaffIdAndPeriodId(string staffId, int periodId);
    }


}
