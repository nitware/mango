using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model;
using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;


namespace Mango.Business
{
    public class StaffInpsLogic : BusinessLogicBase<StaffInps, STAFF_INPS>
    {
        public StaffInpsLogic()
        {
            base.translator = new StaffInpsTranslator();
        }

        public bool Modify(Inps inps)
        {
            try
            {
                Func<STAFF_INPS, bool> predicate = si => si.APPRAISAL_HEADER.Staff_ID == inps.Staff.Id &&  si.Inps_ID == inps.Id;
                STAFF_INPS entity = GetEntityBy(predicate);

                if (entity == null)
                {
                    return true;
                }

                entity.Score = inps.Score;

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


        //public bool Add(List<Customer> customers, long appraisalHeaderId)
        //{
        //    try
        //    {
        //        List<StaffInps> staffInpsList = new List<StaffInps>();
        //        foreach (Customer customer in customers)
        //        {
        //            StaffInps staffInps = new StaffInps();
        //            staffInps.Appraisal = new Appraisal() { Id = appraisalHeaderId };
        //            staffInps.Inps = new Inps() { Id = customer.Id };
        //            staffInps.Score = customer.Score;
        //            staffInpsList.Add(staffInps);
        //        }

        //        if (staffInpsList == null || staffInpsList.Count <= 0)
        //        {
        //            throw new Exception("No INPS found! Please try again or contact your HR department after three unsuccessful trials.");
        //        }

        //        return base.Add(staffInpsList) > 0 ? true : false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



    }



}
