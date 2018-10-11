using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;
using Mango.Model.Model;

namespace Mango.Business
{
    public class StaffMetricLogic : BusinessLogicBase<StaffMetric, STAFF_METRIC>
    {
        public StaffMetricLogic()
        {
            translator = new StaffMetricTranslator();
        }

        public bool Modify(Metrics metrics)
        {
            try
            {
                Func<STAFF_METRIC, bool> predicate = sm => sm.Metric_ID == metrics.Id;
                STAFF_METRIC entity = GetEntityBy(predicate);

                entity.Score = metrics.Score;

                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception(NoItemModified);
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
