using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model.Translator;
using Mango.Data;
using Mango.Model;
using System.Data;
using Mango.Model.Model;
using Mango.Business.DbFacade;

namespace Mango.Business
{
    public class LevelLogic : BusinessLogicBase<Level, JOB_ROLE_LEVEL>
    {
        private StaffLevelLogic staffLevelLogic;
        private CurrentPeriodDbFacade currentPeriodDbFacade;

        public LevelLogic()
        {
            base.translator = new LevelTranslator();
            currentPeriodDbFacade = new CurrentPeriodDbFacade();
            staffLevelLogic = new StaffLevelLogic();
        }

        //public override List<Level> GetAll()
        //{
        //    try
        //    {
        //        Period period = currentPeriodDbFacade.GetCurrentPeriod();
        //        if (period == null)
        //        {
        //            throw new Exception("No current period found! Please contact your system administrator.");
        //        }

        //        Func<STAFF_JOB_ROLE_LEVEL, bool> predicate = s => s.Period_ID == period.Id && s.STAFF.Company_Id == 2;
        //        List<StaffLevel> staffLevels = staffLevelLogic.GetModelsBy(predicate).OrderBy(sl => sl.Staff.FullName).ToList();

        //        List<Level> levels = null;
        //        if (staffLevels != null && staffLevels.Count > 0)
        //        {
        //            levels = new List<Level>();
        //            foreach (StaffLevel staffLevel in staffLevels)
        //            {
        //                Level level = new Level();
        //                level.Id = staffLevel.Level.Id;
        //                level.Name = staffLevel.Level.Name;
        //                levels.Add(level);
        //            }
        //        }

        //        return levels;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool Modify(Level level)
        {
            try
            {
                Func<JOB_ROLE_LEVEL, bool> predicate = j => j.Job_Role_Level_ID == level.Id;
                JOB_ROLE_LEVEL entity = GetEntityBy(predicate);

                entity.Job_Role_Level_ID = level.Id;
                entity.Job_Role_Level_Name = level.Name;

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

        public bool Remove(Level level)
        {
            try
            {
                Func<JOB_ROLE_LEVEL, bool> selector = jr => jr.Job_Role_Level_ID == level.Id;
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
