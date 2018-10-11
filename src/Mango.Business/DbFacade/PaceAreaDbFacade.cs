using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Collections.ObjectModel;
using System.Data;
using Mango.Data;
using Mango.Data.Interfaces;
using Mango.Model;

namespace Mango.Business.DbFacade
{
    public class PaceAreaDbFacade
    {
        private IPaceAreaDb paceAreaDb;

        public PaceAreaDbFacade()
        {
            paceAreaDb = new PaceAreaDb();
        }

        public ObservableCollection<PaceArea> Load()
        {
            try
            {
                ObservableCollection<PaceArea> paceAreas = new ObservableCollection<PaceArea>();

                DataSet dsPaceArea = paceAreaDb.SelectAllPaceArea();
                if (dsPaceArea != null)
                {
                    if (dsPaceArea.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsPaceArea.Tables[0].Rows.Count; i++)
                        {
                            PaceArea paceArea = new PaceArea();
                            paceArea.Id = Convert.ToString(dsPaceArea.Tables[0].Rows[i][paceAreaDb.FIELD_PACE_AREA_ID]);
                            paceArea.Name = Convert.ToString(dsPaceArea.Tables[0].Rows[i][paceAreaDb.FIELD_PACE_NAME]);
                            paceArea.Description = Convert.ToString(dsPaceArea.Tables[0].Rows[i][paceAreaDb.FIELD_PACE_DESCRIPTION]);

                            paceAreas.Add(paceArea);
                        }
                    }
                }

                return paceAreas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        



    }
}