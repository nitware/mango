using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;
using Mango.Data.Interfaces;

namespace Mango.Data
{
    public class GradeScaleDb : DataAccess.DataAccess, IGradeScaleDb
    {
        private const string CLASS_NAME = "GradeScaleDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Company Stored Procedure declaration

        private const string STP_GRADE_SCALE_SELECTGRADE_SCALEBYFROMANDTO = "STP_GRADE_SCALE_SELECTGRADE_SCALEBYFROMANDTO";
        private const string STP_GRADE_SCALE_SELECTALLGRADE_SCALE = "STP_GRADE_SCALE_SELECTALLGRADE_SCALE";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Company Parameter declaration

        //Parameter decleration for GRADE_SCALE_ID
        private const string PARAM_GRADE_SCALE_ID_NAME = "@GradeScaleID";
        private const SqlDbType PARAM_GRADE_SCALE_ID_TYPE = SqlDbType.TinyInt;
        private const int PARAM_GRADE_SCALE_ID_SIZE = 1;

        //Parameter decleration for FROM
        private const string PARAM_FROM_NAME = "@From";
        private const SqlDbType PARAM_FROM_TYPE = SqlDbType.Decimal;
        private const int PARAM_FROM_SIZE = 4;

        //Parameter decleration for COMPANY_DESCRIPTION
        private const string PARAM_TO_NAME = "@To";
        private const SqlDbType PARAM_TO_TYPE = SqlDbType.Decimal;
        private const int PARAM_TO_SIZE = 4;

        #endregion

        //==========================================================================================
        //Company Table Field Name Declaration
        //==========================================================================================
        #region Company Field Name declaration

        public string FIELD_GRADE_SCALE_ID { get { return "Grade_Scale_ID"; } }
        public string FIELD_FROM { get { return "From"; } }
        public string FIELD_TO { get { return "To"; } }

        #endregion

        //Table name declarations for Company in the database, this will be used for dataset reference
        public string GRADE_SCALE_TABLE_NAME = "GRADE_SCALE";

        //==========================================================================================
        //public CompanyDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region CompanyDb Class Methods

        public DataSet SelectGradeScaleByFromAndTo(decimal totalScore)
        {
            //const string METHOD_NAME  = "SelectGradeScaleByFromAndTo";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_FROM_NAME, PARAM_FROM_TYPE, PARAM_FROM_SIZE, totalScore));

                //Execute Stored Procedure
                return ExecuteDataset(STP_GRADE_SCALE_SELECTGRADE_SCALEBYFROMANDTO, param, GRADE_SCALE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectAllGradeScale()
        {
            //const string METHOD_NAME  = "SelectAllGradeScale";

            try
            {
                //Execute Stored Procedure
                return ExecuteDataset(STP_GRADE_SCALE_SELECTALLGRADE_SCALE, null, GRADE_SCALE_TABLE_NAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }


}
