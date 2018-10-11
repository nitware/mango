using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Security.Cryptography;
using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class LoginDetailLogic : BusinessLogicBase<LoginDetail, STAFF_LOGIN>
    {
        private const string DefaultPassword = "welcome";

        public LoginDetailLogic()
        {
            base.translator = new LoginDetailTranslator();
        }

        public LoginDetail Get(string userName, string password)
        {
            try
            {
                Func<STAFF_LOGIN, bool> selector = pl => pl.Username.Equals(userName, StringComparison.OrdinalIgnoreCase);
                LoginDetail loginDetail = GetModelBy(selector);

                if (loginDetail != null)
                {
                    byte[] enteredPasswordHash = CreatePasswordHash(password);
                    if (ComparePassword(enteredPasswordHash, loginDetail.Password))
                    {
                        return loginDetail;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        private static bool ComparePassword(byte[] hash, byte[] oldHash)
        {
            try
            {
                if (hash == null || oldHash == null || hash.Length != oldHash.Length)
                {
                    return false;
                }
                               
                for (int count = 0; count < hash.Length; count++)
                {
                    if (hash[count] != oldHash[count]) return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginDetail Get(string userName)
        {
            try
            {
                Func<STAFF_LOGIN, bool> selector = pl => pl.Username.Equals(userName, StringComparison.OrdinalIgnoreCase);
                return GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginDetail Get(Staff staff)
        {
            try
            {
                Func<STAFF_LOGIN, bool> selector = pl => pl.Staff_ID == staff.Id;
                return GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginDetail Add(Staff staff)
        {
            try
            {
                LoginDetail login = new LoginDetail();
                login.Staff = staff;
                login.Username = CreateUserName(staff);
                login.Password = CreatePasswordHash(DefaultPassword);
                login.IsLocked = false;
                login.IsActivated = false;
                login.IsFirstLogon = true;

                return base.Add(login);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CreateUserName(Staff staff)
        {
            try
            {
                string smallFirstName = staff.FirstName.Substring(1, staff.FirstName.Length - 1).ToLower();
                string smallLastName = staff.LastName.Substring(1, staff.LastName.Length - 1).ToLower();
                string firstNameFirstChar = staff.FirstName.Substring(0, 1).ToUpper();
                string lastNameFirstChar = staff.LastName.Substring(0, 1).ToUpper();

                return firstNameFirstChar + smallFirstName + "." + lastNameFirstChar + smallLastName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(LoginDetail loginDetail)
        {
            try
            {
                Func<STAFF_LOGIN, bool> selector = l => l.Staff_ID == loginDetail.Staff.Id;
                STAFF_LOGIN loginEntity = GetEntityBy(selector);

                if (loginEntity != null)
                {
                    byte[] hash = CreatePasswordHash(DefaultPassword);

                    loginEntity.Password = hash;
                    loginEntity.Is_Locked = false;
                    loginEntity.Is_Activated = true;
                    loginEntity.Is_First_Logon = true;

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
                else
                {
                    return Add(loginDetail.Staff) != null ? true : false;
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

        private static byte[] CreatePasswordHash(string password)
        {
            try
            {
                //string defaultPassword = "welcome";

                HashAlgorithm hashAlg = new SHA512Managed();
                byte[] pwordData = Encoding.Default.GetBytes(password);
                byte[] hash = hashAlg.ComputeHash(pwordData);
                return hash;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(LoginDetail loginDetail)
        {
            try
            {
                Func<STAFF_LOGIN, bool> predicate = c => c.Staff_ID == loginDetail.Staff.Id;
                STAFF_LOGIN loginDetailEntity = GetEntityBy(predicate);

                loginDetailEntity.Username = loginDetail.Username;
                loginDetailEntity.Is_Activated = loginDetail.IsActivated;
                loginDetailEntity.Is_Locked = loginDetail.IsLocked;
               
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

        public LoginDetail ChangePassword(Staff staff, string password)
        {
            try
            {
                Func<STAFF_LOGIN, bool> predicate = c => c.Staff_ID == staff.Id;
                STAFF_LOGIN loginDetailEntity = GetEntityBy(predicate);

                byte[] hash = CreatePasswordHash(password);
               
                loginDetailEntity.Password = hash;
                loginDetailEntity.Is_First_Logon = false;
                
                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return Get(staff);
                }

                return null;
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

        public bool Remove(Staff staff)
        {
            try
            {
                bool suceeded = false;

                Func<STAFF_LOGIN, bool> selector = P => P.Staff_ID == staff.Id;

                LoginDetail loginDetail = GetModelBy(selector);
                if (loginDetail != null)
                {
                    suceeded = base.Remove(selector);
                    repository.SaveChanges();
                }

                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }

}
