using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;
using Mango.Data.Interfaces;

namespace Mango.Data
{
    public class PaceRatingDb : DataAccess.DataAccess, IPaceRatingDb
    {
        private const string CLASS_NAME = "PaceRatingDb";

        //==========================================================================================
        //Db Stored Procedures declaration
        //==========================================================================================
        #region  Company Stored Procedure declaration

        private const string STP_PACE_RATING_SELECTPACE_RATINGBYFROMANDTO = "STP_PACE_RATING_SELECTPACE_RATINGBYFROMANDTO";
        private const string STP_PACE_RATING_SELECTALLPACE_RATING = "STP_PACE_RATING_SELECTALLPACE_RATING";

        #endregion

        //==========================================================================================
        //Db Configuration properties
        //==========================================================================================
        #region Company Parameter declaration

        //Parameter decleration for PACE_RATING_ID
        private const string PARAM_PACE_RATING_ID_NAME = "@PaceRatingID";
        private const SqlDbType PARAM_PACE_RATING_ID_TYPE = SqlDbType.Int;
        private const int PARAM_PACE_RATING_ID_SIZE = 4;

        //Parameter decleration for RATING
        private const string PARAM_RATING_NAME = "@Rating";
        private const SqlDbType PARAM_RATING_TYPE = SqlDbType.NChar;
        private const int PARAM_RATING_SIZE = 2;

        //Parameter decleration for FROM
        private const string PARAM_FROM_NAME = "@From";
        private const SqlDbType PARAM_FROM_TYPE = SqlDbType.Decimal;
        private const int PARAM_FROM_SIZE = 1;

        //Parameter decleration for COMPANY_DESCRIPTION
        private const string PARAM_TO_NAME = "@To";
        private const SqlDbType PARAM_TO_TYPE = SqlDbType.Decimal;
        private const int PARAM_TO_SIZE = 1;

        //Parameter decleration for GRDAE
        private const string PARAM_GRADE_NAME = "@Grade";
        private const SqlDbType PARAM_GRADE_TYPE = SqlDbType.TinyInt;
        private const int PARAM_GRADE_SIZE = 1;

        //Parameter decleration for DEFINITION
        private const string PARAM_DEFINITION_NAME = "@Definition";
        private const SqlDbType PARAM_DEFINITION_TYPE = SqlDbType.VarChar;
        private const int PARAM_DEFINITION_SIZE = 150;

        #endregion

        //==========================================================================================
        //Company Table Field Name Declaration
        //==========================================================================================
        #region Company Field Name declaration

        public string FIELD_PACE_RATING_ID { get { return "Grade_Scale_ID"; } }
        public string FIELD_RATING { get { return "Rating"; } }
        public string FIELD_FROM { get { return "From"; } }
        public string FIELD_TO { get { return "To"; } }
        public string FIELD_GRADE { get { return "Grade"; } }
        public string FIELD_DEFINITION { get { return "Definition"; } }

        #endregion

        //Table name declarations for Company in the database, this will be used for dataset reference
        public string PACE_RATING_TABLE_NAME = "PACE_RATING";

        //==========================================================================================
        //public CompanyDb Class Method declarations that will be called from the Biz Tier
        //==========================================================================================
        #region CompanyDb Class Methods

        public DataSet SelectPaceRatingByFromAndTo(decimal totalScore)
        {
            //const string METHOD_NAME  = "SelectPaceRatingByFromAndTo";

            try
            {
                //Method parameter declaration
                ArrayList param = new ArrayList();
                param.Add(MakeParam(PARAM_FROM_NAME, PARAM_FROM_TYPE, PARAM_FROM_SIZE, totalScore));

                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_RATING_SELECTPACE_RATINGBYFROMANDTO, param, PACE_RATING_TABLE_NAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SelectAllPaceRating()
        {
            //const string METHOD_NAME  = "SelectAllPaceRating";

            try
            {
                //Execute Stored Procedure
                return ExecuteDataset(STP_PACE_RATING_SELECTALLPACE_RATING, null, PACE_RATING_TABLE_NAME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }



}
