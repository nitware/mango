using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Mango.Data.Interfaces;
using Mango.Data;
using Mango.Model;

namespace Mango.Business.DbFacade
{
    public class OptionDbFacade
    {
        private IOptionDb optionDb;

        public OptionDbFacade()
        {
            optionDb = new OptionDb();
        }

        public List<Option> Load()
        {
            try
            {
                List<Option> options = new List<Option>();
                DataSet dsOption = optionDb.SelectAllOption();

                if (dsOption != null)
                {
                    if (dsOption.Tables[0].Rows.Count > 0)
                    {
                        Option op = new Option();
                        op.Id = 0;
                        op.Name = "< Select Option >";
                        options.Add(op);

                        for (int i = 0; i < dsOption.Tables[0].Rows.Count; i++)
                        {
                            Option option = new Option();
                            option.Id = Convert.ToByte(dsOption.Tables[0].Rows[i][optionDb.FIELD_OPTION_ID]);
                            option.Name = Convert.ToString(dsOption.Tables[0].Rows[i][optionDb.FIELD_OPTION_NAME]);

                            options.Add(option);
                        }
                    }
                }

                return options;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}