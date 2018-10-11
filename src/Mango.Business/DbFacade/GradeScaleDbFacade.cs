using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;

namespace Mango.Business.DbFacade
{
    public class GradeScaleDbFacade
    {
        private IGradeScaleDb gradeScaleDb;

        public GradeScaleDbFacade()
        {
            gradeScaleDb = new GradeScaleDb();
        }

        public GradeScale GetGrade(int score)
        {
            try
            {
                GradeScale grade = new GradeScale();
                DataSet dsGrade = gradeScaleDb.SelectGradeScaleByFromAndTo(score);
                if (dsGrade != null)
                {
                    if (dsGrade.Tables[0].Rows.Count > 0)
                    {
                        grade.Id = Convert.ToByte(dsGrade.Tables[0].Rows[0][gradeScaleDb.FIELD_GRADE_SCALE_ID]);
                    }
                }

                return grade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GradeScale> Load()
        {
            try
            {
                List<GradeScale> grades = new List<GradeScale>();
                DataSet dsGrade = gradeScaleDb.SelectAllGradeScale();
                if (dsGrade != null)
                {
                    if (dsGrade.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsGrade.Tables[0].Rows.Count; i++)
                        {
                            GradeScale grade = new GradeScale();
                            grade.Id = Convert.ToByte(dsGrade.Tables[0].Rows[i][gradeScaleDb.FIELD_GRADE_SCALE_ID]);
                            grade.From = Convert.ToDecimal(dsGrade.Tables[0].Rows[i][gradeScaleDb.FIELD_FROM]);
                            grade.To = Convert.ToDecimal(dsGrade.Tables[0].Rows[i][gradeScaleDb.FIELD_TO]);

                            grades.Add(grade);
                        }
                    }
                }

                return grades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }


}