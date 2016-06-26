using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using OpenIdentityCustomService.User;
using SageFrame.Common;
using SageFrame.Security;
using SageFrame.Security.Controllers;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using SageFrame.Web.Utilities;

namespace OpenIdentityCustomService.Services
{
    public class SfUserService
    {

        public SfUser IsValidUser(string userName, string password)
        {
            try
            {


                SfUserProvider sfUserProvider = new SfUserProvider();
                SfUser user = sfUserProvider.GetUserDetail(userName);
                if (user != null)
                {
                    if (!(string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password)))
                    {
                        if (PasswordHelper.ValidateUser(user.PasswordFormat, password, user.Password, user.PasswordSalt))
                        {
                            return user;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public SfUserDetail GetProfile(string userName)
        {
            try
            {
                SfUserProvider sfUserProvider = new SfUserProvider();
                return sfUserProvider.GetUserProfile(userName);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        internal SfUser IsValidUser(string userName)
        {
            try
            {


                SfUserProvider sfUserProvider = new SfUserProvider();
                SfUser user = sfUserProvider.GetUserDetail(userName);
                if (user != null)
                    return user;
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class SfUserProvider
    {

        public SfUser GetUserDetail(string userName)
        {
            try
            {

                string sp = "[dbo].[usp_OpenIdentity_GetUserDetail]";
                SQLHandler sagesql = new SQLHandler();

                List<KeyValuePair<string, object>> parammeters = new List<KeyValuePair<string, object>>();
                parammeters.Add(new KeyValuePair<string, object>("@UserName", userName));
                return sagesql.ExecuteAsObject<SfUser>(sp, parammeters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public SfUserDetail GetUserProfile(string userName)
        {
            try
            {

                string sp = "[dbo].[usp_OpenIdentity_GetUserProfile]";
                SQLHandler sagesql = new SQLHandler();

                List<KeyValuePair<string, object>> parammeters = new List<KeyValuePair<string, object>>();
                parammeters.Add(new KeyValuePair<string, object>("@UserName", userName));
                return sagesql.ExecuteAsObject<SfUserDetail>(sp, parammeters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
