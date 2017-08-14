using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GlobalDoctorInventoryServer
{
    internal class DAUser
    {
        private GDIM_DataEntities context;
        public IEnumerable<GDIM_User> GetUsers(Func<GDIM_User, bool> Conditions)
        {
            try
            {
                using ( context = new GDIM_DataEntities())
                {
                    IEnumerable<GDIM_User> users = context.GDIM_User.Where(Conditions);
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteUsers(GDIM_User userToDelete)
        {
            int delUserNum = -1;

            return delUserNum;
        }

        public int UpdateUsers(GDIM_User userToUpdate)
        {
            GDIM_User originUser = null;
            using (context = new GDIM_DataEntities())
            {
                originUser = context.GDIM_User.Where(x => x.User_ID == userToUpdate.User_ID)
                    .FirstOrDefault<GDIM_User>();
                originUser.User_Image = userToUpdate.User_Image;
            }
        }

        public string InsertUsers()
        {
            string key = string.Empty;
            return key;
        }
    }
}
