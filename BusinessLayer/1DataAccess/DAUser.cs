using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Transactions;
using BusinessLayer.Entities;

namespace BusinessLayer
{
    internal class DAUser
    {
        /// <summary>
        /// Use to commit transaction
        /// </summary>
        public GDIM_DataEntities Context
        {
            get
            {
                if (Context == null)
                    Context = new GDIM_DataEntities();
                return Context;
            }
            set
            {
                Context = value;
            }
        }
        /// <summary>
        /// get user
        /// </summary>
        /// <param name="Conditions"></param>
        /// <returns></returns>
        public IEnumerable<GDIM_User> GetUsers(Func<GDIM_User, bool> Conditions)
        {
            try
            {
                using (Context)
                {
                    IEnumerable<GDIM_User> users = Context.GDIM_User.Where(Conditions);
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("get users message failed "+ex.Message);
            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userToDelete"></param>
        /// <returns></returns>
        public bool DeleteUsers(GDIM_User userToDelete)
        {
            try
            {
                using (Context)
                {
                    Context.GDIM_User.Remove(userToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("delete user failed" + ex.Message);
            }
            return true;
        }

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        public bool UpdateUsers(GDIM_User userToUpdate)
        {
            try
            {
                GDIM_User originUser = null;
                using (Context)
                {

                    originUser = Context.GDIM_User.Where(x => x.User_ID == userToUpdate.User_ID)
                                .FirstOrDefault<GDIM_User>();
                    EntityHelper.ShallowCopy<GDIM_User>(originUser, userToUpdate);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("update user failed" + ex.Message);
            }
        }

        /// <summary>
        /// insert user
        /// </summary>
        /// <param name="userToInsert"></param>
        /// <returns></returns>
        public bool InsertUsers(GDIM_User userToInsert)
        {
            try
            {
                using (Context)
                {
                    Context.GDIM_User.Add(userToInsert);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("insert user failed" + ex.Message);
            }
        }
    }
}
