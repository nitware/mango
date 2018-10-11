using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.ObjectModel;
using System.Data;
using Mango.Data.Interfaces;
using Mango.Data;
using Mango.Model;
using Mango.DataAccess;

namespace Mango.Business.DbFacade
{
    public class PaceDbFacade
    {
        private IPaceDb paceDb;

        public PaceDbFacade()
        {
            paceDb = new PaceDb();
        }

        public List<Pace> LoadDefaultPace(bool isSupervisor, int periodId)
        {
            List<Pace> paces = new List<Pace>();

            DataSet dsPace = paceDb.SelectDefaultPace(periodId);
            if (dsPace != null)
            {
                if (dsPace.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsPace.Tables[0].Rows.Count; i++)
                    {
                        Pace pace = new Pace();
                        pace.PaceAreaId = dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_AREA_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_AREA_ID]);
                        pace.Id = dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_ID]);
                        pace.AppraisalHeaderId = dsPace.Tables[0].Rows[i][paceDb.FIELD_APPRAISAL_HEADER_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_APPRAISAL_HEADER_ID]);
                        pace.Weight = dsPace.Tables[0].Rows[i][paceDb.FIELD_WEIGHT] == DBNull.Value ? 0 : Convert.ToDecimal(dsPace.Tables[0].Rows[i][paceDb.FIELD_WEIGHT]);
                        pace.Score = dsPace.Tables[0].Rows[i][paceDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(dsPace.Tables[0].Rows[i][paceDb.FIELD_SCORE]);
                        pace.Justification = dsPace.Tables[0].Rows[i][paceDb.FIELD_JUSTIFICATION] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i][paceDb.FIELD_JUSTIFICATION]);
                        pace.Name = dsPace.Tables[0].Rows[i]["Pace_Name"] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i]["Pace_Name"]);
                        pace.Description = dsPace.Tables[0].Rows[i]["Pace_Description"] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i]["Pace_Description"]);
                        pace.Grade = dsPace.Tables[0].Rows[i]["Grade"] == DBNull.Value ? Convert.ToByte(0) : Convert.ToByte(dsPace.Tables[0].Rows[i]["Grade"]);
                        pace.IsSupervisor = isSupervisor;
                       
                        ////temp code
                        //pace.Justification = UtilityLogic.JumbbleText(pace.Justification);
                        //pace.Description = UtilityLogic.JumbbleText(pace.Description);

                        paces.Add(pace);
                    }
                }
            }

            return paces;
        }

        public List<Pace> LoadPaceByStaffAndPeriod(string staffId, int periodId, bool isSupervisor)
        {
            List<Pace> paces = new List<Pace>();

            DataSet dsPace = paceDb.SelectPaceByStaffIDAndPeriodID(staffId, periodId);
            if (dsPace != null)
            {
                if (dsPace.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsPace.Tables[0].Rows.Count; i++)
                    {
                        Pace pace = new Pace();
                        pace.PaceAreaId = dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_AREA_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_AREA_ID]);
                        pace.Id = dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_PACE_ID]);
                        pace.AppraisalHeaderId = dsPace.Tables[0].Rows[i][paceDb.FIELD_APPRAISAL_HEADER_ID] == DBNull.Value ? 0 : Convert.ToInt32(dsPace.Tables[0].Rows[i][paceDb.FIELD_APPRAISAL_HEADER_ID]);
                        pace.Weight = dsPace.Tables[0].Rows[i][paceDb.FIELD_WEIGHT] == DBNull.Value ? 0 : Convert.ToDecimal(dsPace.Tables[0].Rows[i][paceDb.FIELD_WEIGHT]);
                        pace.Score = dsPace.Tables[0].Rows[i][paceDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(dsPace.Tables[0].Rows[i][paceDb.FIELD_SCORE]);
                        pace.Justification = dsPace.Tables[0].Rows[i][paceDb.FIELD_JUSTIFICATION] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i][paceDb.FIELD_JUSTIFICATION]);
                        pace.Name = dsPace.Tables[0].Rows[i]["Pace_Name"] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i]["Pace_Name"]);
                        pace.Description = dsPace.Tables[0].Rows[i]["Pace_Description"] == DBNull.Value ? null : Convert.ToString(dsPace.Tables[0].Rows[i]["Pace_Description"]);
                        pace.Grade = dsPace.Tables[0].Rows[i]["Grade"] == DBNull.Value ? Convert.ToByte(0) : Convert.ToByte(dsPace.Tables[0].Rows[i]["Grade"]);
                        pace.IsSupervisor = isSupervisor;
                       
                        paces.Add(pace);
                    }
           
                }
            }

            return paces;
        }

        public bool CreatePace(long appraisalHeaderId, Pace pace, Transaction transaction)
        {
            try
            {
                if (pace != null)
                {
                    if (paceDb.InsertPace(pace.Id, pace.PaceAreaId, appraisalHeaderId, pace.Weight, pace.Score, pace.Justification, transaction))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyPace(Pace pace, Transaction transaction)
        {
            try
            {
                if (pace != null)
                {
                    if (paceDb.UpdatePaceByPaceId(pace.Id, pace.PaceAreaId, pace.AppraisalHeaderId, pace.Weight, pace.Score, pace.Justification, transaction))
                    {
                        return true;
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}