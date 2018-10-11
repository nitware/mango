using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;
using Mango.Model;
using Mango.Business.DbFacade;

namespace Mango.Business
{
    public class StaffLearningLogic : BusinessLogicBase<StaffLearning, STAFF_LEARNING>
    {
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public StaffLearningLogic()
        {
            base.translator = new StaffLearningTranslator();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
        }

        public override List<StaffLearning> GetAll()
        {
            try
            {
                Period period = currentPeriodDbFacade.GetCurrentPeriod();
                if (period == null)
                {
                    throw new Exception("No current period found! Please contact your system administrator.");
                }

                Func<STAFF_LEARNING, bool> predicate = s => s.Period_ID == period.Id && s.STAFF.Company_Id == 2 && s.STAFF.Is_Active == true;
                return base.GetModelsBy(predicate).OrderBy(sl => sl.Staff.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool Modify(StaffLearning staffLearning)
        {
            try
            {
                Func<STAFF_LEARNING, bool> predicate = s => s.Staff_ID == staffLearning.Staff.Id && s.Period_ID == staffLearning.Period.Id;
                STAFF_LEARNING entity = GetEntityBy(predicate);
                
                entity.Training_Score = staffLearning.TrainingScore;
                entity.Percentage_Score = staffLearning.PercentScore;

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

        public bool Remove(StaffLearning staffLearning)
        {
            try
            {
                Func<STAFF_LEARNING, bool> predicate = s => s.Staff_ID == staffLearning.Staff.Id && s.Period_ID == staffLearning.Period.Id;
                bool suceeded = base.Remove(predicate);
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
