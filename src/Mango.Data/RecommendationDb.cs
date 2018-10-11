//==================================================================================================
//Programmer: Daniel Egenti U.
//Date: 22/07/2011 09:12:14

//Description: This Class represents the data tier layer class for Recommendation table.
//It contains all data access methods and static constants representing the
//Stored Procedures, field names and SQL parameters required by this entity.

//No man can cover the moon with his bare hands. You will shine when the time is ripe.
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using Mango.Data.Interfaces;
using Mango.DataAccess;


namespace Mango.Data
{
    public class RecommendationDb : DataAccess.DataAccess, IRecommendationDb
    {
		private const string CLASS_NAME = "RecommendationDb";

		//==========================================================================================
		//Db Stored Procedures declaration
		//==========================================================================================
		#region  Recommendation Stored Procedure declaration

		private const string STP_RECOMMENDATION_INSERTRECOMMENDATION = "STP_RECOMMENDATION_INSERTRECOMMENDATION";
		private const string STP_RECOMMENDATION_DELETERECOMMENDATIONBYRECOMMENDATION_ID = "STP_RECOMMENDATION_DELETERECOMMENDATIONBYRECOMMENDATION_ID";
		private const string STP_RECOMMENDATION_UPDATERECOMMENDATIONBYRECOMMENDATION_ID = "STP_RECOMMENDATION_UPDATERECOMMENDATIONBYRECOMMENDATION_ID";
		private const string STP_RECOMMENDATION_SELECTALLRECOMMENDATION = "STP_RECOMMENDATION_SELECTALLRECOMMENDATION";
		private const string STP_RECOMMENDATION_SELECTRECOMMENDATIONBYRECOMMENDATION_ID = "STP_RECOMMENDATION_SELECTRECOMMENDATIONBYRECOMMENDATION_ID";

		#endregion

		//==========================================================================================
		//Db Configuration properties
		//==========================================================================================
		#region Recommendation Parameter declaration 

		//Parameter decleration for RECOMMENDATION_ID
		private const string PARAM_RECOMMENDATION_ID_NAME = "@RecommendationID";
		private const SqlDbType PARAM_RECOMMENDATION_ID_TYPE = SqlDbType.TinyInt;
		private const int PARAM_RECOMMENDATION_ID_SIZE = 1;

		//Parameter decleration for RECOMMENDATION_NAME
		private const string PARAM_RECOMMENDATION_NAME_NAME = "@RecommendationName";
		private const SqlDbType PARAM_RECOMMENDATION_NAME_TYPE = SqlDbType.VarChar;
		private const int PARAM_RECOMMENDATION_NAME_SIZE = 50;

		#endregion

		//==========================================================================================
		//Recommendation Table Field Name Declaration
		//==========================================================================================
		#region Recommendation Field Name declaration 

		public string FIELD_RECOMMENDATION_ID { get { return "Recommendation_ID"; } }
		public string FIELD_RECOMMENDATION_NAME { get { return "Recommendation_Name"; } }

		#endregion

		//Table name declarations for Recommendation in the database, this will be used for dataset reference
		public string RECOMMENDATION_TABLE_NAME  = "RECOMMENDATION";

		//==========================================================================================
		//public RecommendationDb Class Method declarations that will be called from the Biz Tier
		//==========================================================================================
		#region RecommendationDb Class Methods 

		public bool InsertRecommendation(byte recommendationId, string recommendationName, Transaction transaction)
		{
			//const string METHOD_NAME  = "InsertRecommendation";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));
				param.Add(MakeParam(PARAM_RECOMMENDATION_NAME_NAME, PARAM_RECOMMENDATION_NAME_TYPE, PARAM_RECOMMENDATION_NAME_SIZE, recommendationName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_RECOMMENDATION_INSERTRECOMMENDATION, param, transaction) == 0)
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public bool DeleteRecommendationByRecommendationId(byte recommendationId, Transaction transaction)
		{
			//const string METHOD_NAME  = "DeleteRecommendationByRecommendationId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));

				//Execute Stored Procedure
				if (ExecuteProc(STP_RECOMMENDATION_DELETERECOMMENDATIONBYRECOMMENDATION_ID, param, transaction) == 0)
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public bool UpdateRecommendationByRecommendationId(byte recommendationId, string recommendationName, Transaction transaction)
		{
			//const string METHOD_NAME  = "UpdateRecommendationByRecommendationId";

			try
			{
				//Make parameter(s)
				ArrayList param = new ArrayList();
				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));
				param.Add(MakeParam(PARAM_RECOMMENDATION_NAME_NAME, PARAM_RECOMMENDATION_NAME_TYPE, PARAM_RECOMMENDATION_NAME_SIZE, recommendationName));

				//Execute Stored Procedure
				if (ExecuteProc(STP_RECOMMENDATION_UPDATERECOMMENDATIONBYRECOMMENDATION_ID, param, transaction) == 0)
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectAllRecommendation()
		{
			//const string METHOD_NAME  = "SelectAllRecommendation";

			try
			{
				//Execute Stored Procedure
				return ExecuteDataset(STP_RECOMMENDATION_SELECTALLRECOMMENDATION, null, RECOMMENDATION_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		public DataSet SelectRecommendationByRecommendationId(byte recommendationId)
		{
			//const string METHOD_NAME  = "SelectRecommendationByRecommendationId";

			try
			{
				//Method parameter declaration
				ArrayList param = new ArrayList();

				param.Add(MakeParam(PARAM_RECOMMENDATION_ID_NAME, PARAM_RECOMMENDATION_ID_TYPE, PARAM_RECOMMENDATION_ID_SIZE, recommendationId));

				//Execute Stored Procedure
				return ExecuteDataset(STP_RECOMMENDATION_SELECTRECOMMENDATIONBYRECOMMENDATION_ID, param, RECOMMENDATION_TABLE_NAME);
			}
			catch (Exception ex)
			{
				//Me.ErrorLog("Error has occurred in " & CLASS_NAME & ":" & METHOD_NAME & " method " & VbCrLf & ex.Message & VbCrLf & ex.StackTrace)
				throw ex;
			}
		}

		#endregion

	}
}


