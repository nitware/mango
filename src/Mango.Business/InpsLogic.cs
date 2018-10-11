using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using Mango.Model;
using System.Data;
using System.Transactions;

namespace Mango.Business
{
    public class InpsLogic : BusinessLogicBase<Inps, INPS>
    {
        private StaffLogic staffLogic;
        private DepartmentLogic departmentLogic;
        private StaffInpsLogic staffInpsLogic;

        public InpsLogic()
        {
            base.translator = new InpsTranslator();

            staffLogic = new StaffLogic();
            departmentLogic = new DepartmentLogic();
            staffInpsLogic = new StaffInpsLogic();
        }

        public List<Inps> GetBy(Staff staff, Period period)
        {
            try
            {
                Func<INPS, bool> selector = nps => nps.Staff_ID == staff.Id && nps.Period_ID == period.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Inps> GetBy(Period period)
        {
            try
            {
                Func<INPS, bool> selector = nps => nps.Period_ID == period.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Inps> GetBy(Period period, InpsType inpsType)
        {
            try
            {
                Func<INPS, bool> selector = nps => nps.Period_ID == period.Id && nps.Inps_Type_Id == inpsType.Id;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Add(List<Inps> inpss)
        {
            try
            {
                string alreadyExistMessage = AlreadyExist(inpss[0].Period, inpss[0].Type);
                if (alreadyExistMessage != null)
                {
                    throw new Exception(alreadyExistMessage);
                }

                string staffOkMessage = IsStaffIdsOk(inpss);
                if (staffOkMessage != null)
                {
                    throw new Exception(staffOkMessage);
                }

                string departmentOkMessage = IsDepartmentIdsOk(inpss);
                if (departmentOkMessage != null)
                {
                    throw new Exception(departmentOkMessage);
                }

                return base.Add(inpss);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string AlreadyExist(Period period, InpsType inpsType)
        {
            try
            {
                string message = null;

                Func<INPS, bool> selector = inps => inps.Period_ID == period.Id && inps.Inps_Type_Id == inpsType.Id;
                List<Inps> inpss = base.GetModelsBy(selector);

                if (inpss != null && inpss.Count > 0)
                {
                    message = inpsType.Name + " already exist for " + period.Name + "! ";
                }

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string IsStaffIdsOk(List<Inps> inpss)
        {
            try
            {
                string message = null;
               
                foreach (Inps inps in inpss)
                {
                    Staff staff = staffLogic.Get(inps.Staff.Id.Trim());
                    if (staff == null)
                    {
                        if (message == null)
                        {
                            message += "The following staff IDs does not exist!\n" + inps.Staff.Id;
                        }
                        else
                        {
                            message += "\n" + inps.Staff.Id;
                        }
                    }
                }

                List<string> duplicateStaffs = new List<string>();
                foreach (Inps inps in inpss)
                {
                    List<Inps> duplicate = inpss.Where(i => i.Staff.Id.Trim() == inps.Staff.Id.Trim()).ToList();
                    if (duplicate != null && duplicate.Count > 1)
                    {
                        if (!duplicateStaffs.Contains(inps.Staff.Id))
                        {
                            duplicateStaffs.Add(inps.Staff.Id);
                        }

                        //if (message == null)
                        //{
                        //    message += "Duplicate found for Staff ID! " + inps.Staff.Id;
                        //}
                        //else
                        //{
                        //    message += "\nDuplicate found for Staff ID! " + inps.Staff.Id;
                        //}
                    }
                }

                if (duplicateStaffs != null && duplicateStaffs.Count > 0)
                {
                    for (int i = 0; i < duplicateStaffs.Count; i++)
                    {
                        if (message == null)
                        {
                            message += "Duplicate found for Staff ID! " + duplicateStaffs[i];
                        }
                        else
                        {
                            message += "\nDuplicate found for Staff ID! " + duplicateStaffs[i];
                        }
                    }
                }

                if (message != null)
                {
                    message += "\n\nKindly edit the source file and re-upload.";
                }

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string IsDepartmentIdsOk(List<Inps> inpss)
        {
            try
            {
                string message = null;

                foreach (Inps inps in inpss)
                {
                    Department department = departmentLogic.GetBy(inps.ResponsibleDepartment.Id);
                    if (department == null)
                    {
                        if (message == null)
                        {
                            message += "The following department IDs does not exist!\n" + inps.ResponsibleDepartment.Id;
                        }
                        else
                        {
                            message += "\n" + inps.ResponsibleDepartment.Id;
                        }
                    }
                }

                if (message != null)
                {
                    message += "\n\nKindly edit the source file and re-upload.";
                }

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(Inps inps)
        {
            try
            {
                Func<INPS, bool> predicate = d => d.Inps_ID == inps.Id;
                INPS entity = GetEntityBy(predicate);

                entity.Inps_Type_Id = inps.Type.Id;
                entity.Staff_ID = inps.Staff.Id;
                entity.Kpi = inps.Kpi;
                entity.Measure = inps.Measure;
                entity.Data_Source = inps.DataSource;
                entity.Responsible_Department_ID = inps.ResponsibleDepartment.Id;
                entity.Target = inps.Target;
                entity.Score = inps.Score;
                entity.Period_ID = inps.Period.Id;

                using (TransactionScope transaction = new TransactionScope())
                {
                    int rowsAffected = repository.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        if (staffInpsLogic.Modify(inps))
                        {
                            transaction.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new Exception(NoItemModified);
                    }

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

        public bool Remove(Inps inps)
        {
            try
            {
                Func<INPS, bool> selector = d => d.Inps_ID == inps.Id;
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
