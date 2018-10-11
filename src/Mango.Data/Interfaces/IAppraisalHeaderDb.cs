using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.DataAccess;

namespace Mango.Data.Interfaces
{
    public interface IAppraisalHeaderDb
    {
        string FIELD_APPRAISAL_HEADER_ID { get; }
        string FIELD_PERIOD_ID { get; }
        string FIELD_STAFF_ID { get; }
        string FIELD_SUPERVISOR_ID { get; }
        string FIELD_APPRAISAL_DATE { get; }
        string FIELD_STAFF_RESPONSE_DATE { get; }
        string FIELD_HOD_ID { get; }
        string FIELD_HOD_APPRAISAL_DATE { get; }
        int InsertAppraisalHeader(int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime? staffResponseDate, string hodId, DateTime? hodAppraisalDate, byte statusId, Transaction transaction);
        bool DeleteAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId, Transaction transaction);
        bool DeleteAppraisalHeaderByPeriodId(int periodId, Transaction transaction);
        bool DeleteAppraisalHeaderByStaffId(string staffId, Transaction transaction);
        bool DeleteAppraisalHeaderBySupervisorId(string supervisorId, Transaction transaction);
        bool DeleteAppraisalHeaderByHodId(string hodId, Transaction transaction);
        bool DeleteAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID(long appraisalHeaderId, int periodId, string staffId, string supervisorId, string hodId, Transaction transaction);
        bool UpdateAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime? staffResponseDate, string hodId, DateTime? hodAppraisalDate, byte statusId, Transaction transaction);
        bool UpdateAppraisalHeaderByPeriodId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction);
        bool UpdateAppraisalHeaderByStaffId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction);
        bool UpdateAppraisalHeaderBySupervisorId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction);
        bool UpdateAppraisalHeaderByHodId(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction);
        bool UpdateAppraisalHeaderByAppraisal_Header_IDAndPeriod_IDAndStaff_IDAndSupervisor_IDAndHod_ID(long appraisalHeaderId, int periodId, string staffId, string supervisorId, DateTime appraisalDate, DateTime staffResponseDate, string hodId, DateTime hodAppraisalDate, Transaction transaction);
        DataSet SelectAllAppraisalHeader();
        DataSet SelectAppraisalHeaderByAppraisalHeaderId(long appraisalHeaderId);
        DataSet SelectAppraisalHeaderByPeriodId(int periodId);
        DataSet SelectAppraisalHeaderByStaffId(string staffId);
        DataSet SelectAppraisalHeaderBySupervisorId(string supervisorId);
        DataSet SelectAppraisalHeaderByHodId(string hodId);
        DataSet SelectAppraisalHeaderByPeriodIDAndStaffID(int periodId, string staffId);
       

    }
}
