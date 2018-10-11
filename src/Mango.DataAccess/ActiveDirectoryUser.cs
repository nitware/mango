using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.DirectoryServices;

namespace Mango.DataAccess
{
    public class ActiveDirectoryUser
    {
        private static string ad = ConfigurationManager.ConnectionStrings["AD"].ToString();

        public static bool Authenticate(string domain, string userName, string password)
        {
            string serverName = ad;
            string LDAPATH = "LDAP://" + serverName;
            string domainAndUserName = domain + @"\" + userName;
            DirectoryEntry entry = new DirectoryEntry(LDAPATH, domainAndUserName, password);

            try
            {
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + userName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (result == null)
                {
                    return false;
                }

                LDAPATH = result.Path;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
