using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.ComponentModel.Composition;
using BusinessLayer.Entities;

namespace BusinessLayer
{
    [Export(typeof(IBLUser))]
    public class BLUser:IBLUser
    {
        public IEnumerable<GDIM_User> GetUsers(Func<GDIM_User, bool> conditions)
        {
            try
            {
                DAUser daUser = new DAUser();
                return daUser.GetUsers(conditions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GDIM_User> SaveUser(List<GDIM_User> usersToSave)
        {
            try
            {
                DAUser daUser = new DAUser();
                using (Trans = new TransactionScope())
                {
                    foreach (GDIM_User curUser in usersToSave)
                    {
                        if (string.IsNullOrEmpty(curUser.User_ID))
                        {
                            //insert
                            daUser.InsertUsers(curUser);
                        }
                        else
                        {
                            //update
                            daUser.UpdateUsers(curUser);
                        }
                        daUser.Context.SaveChanges();//template saving
                    }
                    Trans.Complete();
                }
                return usersToSave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUsers(List<GDIM_User> usersToDelete)
        {
            try
            {
                DAUser daUser = new DAUser();
                using (Trans = new TransactionScope())
                {
                    foreach (GDIM_User curUser in usersToDelete)
                    {
                        daUser.DeleteUsers(curUser);
                        daUser.Context.SaveChanges();
                    }
                    Trans.Complete();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransactionScope Trans
        {
            get
            {
                return Trans;
            }
            set
            {
                Trans = value;
            }
        }
    }
}
