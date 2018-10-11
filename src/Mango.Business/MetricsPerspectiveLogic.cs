using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class MetricsPerspectiveLogic : BusinessLogicBase<MetricsPerspective, METRIC_PERSPECTIVE>
    {
        public MetricsPerspectiveLogic()
        {
            base.translator = new MetricsPerspectiveTranslator();
        }

        public bool Modify(MetricsPerspective metricsPerspective)
        {
            try
            {
                Func<METRIC_PERSPECTIVE, bool> selector = d => d.Metric_Perspective_ID == metricsPerspective.Id;
                METRIC_PERSPECTIVE entity = GetEntityBy(selector);

                entity.Metric_Perspective_Name = metricsPerspective.Name;

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

        public bool Remove(MetricsPerspective metricsPerspective)
        {
            try
            {
                Func<METRIC_PERSPECTIVE, bool> selector = d => d.Metric_Perspective_ID == metricsPerspective.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }




}
