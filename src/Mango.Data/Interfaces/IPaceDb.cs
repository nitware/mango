using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IPaceDb
    {
        string FIELD_PACE_ID { get; }
        string FIELD_PACE_AREA_ID { get; }
        string FIELD_APPRAISAL_HEADER_ID { get; }
        string FIELD_WEIGHT { get; }
        string FIELD_SCORE { get; }
        string FIELD_JUSTIFICATION { get; }

        bool InsertPace(int paceId, int paceAreaId, long appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction);
        bool DeletePaceByPaceId(int paceId, Transaction transaction);
        bool DeletePaceByPaceAreaId(int paceAreaId, Transaction transaction);
        bool DeletePaceByAppraisalHeaderId(int appraisalHeaderId, Transaction transaction);
        bool DeletePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId, Transaction transaction);
        bool UpdatePaceByPaceId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction);
        bool UpdatePaceByPaceAreaId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction);
        bool UpdatePaceByAppraisalHeaderId(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction);
        bool UpdatePaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId, decimal weight, decimal score, string justification, Transaction transaction);
        DataSet SelectAllPace();
        DataSet SelectPaceByPaceId(int paceId);
        DataSet SelectPaceByPaceAreaId(int paceAreaId);
        DataSet SelectPaceByAppraisalHeaderId(int appraisalHeaderId);
        DataSet SelectPaceByPace_IDAndPace_Area_IDAndAppraisal_Header_ID(int paceId, int paceAreaId, int appraisalHeaderId);

        DataSet SelectPaceByStaffIDAndPeriodID(string staffId, int periodId);
        DataSet SelectDefaultPace(int periodId);
       
      

    }
}
