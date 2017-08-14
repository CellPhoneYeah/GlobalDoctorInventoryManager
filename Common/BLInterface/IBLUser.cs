using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IBLUser
    {
        /// <summary>
        /// to get users
        /// </summary>
        /// <param name="Conditions"></param>
        /// <returns></returns>
        IEnumerable<GDIM_User> GetUsers(Func<GDIM_User, bool> Conditions);

        /// <summary>
        /// to save users
        /// </summary>
        /// <param name="usersToSave"></param>
        /// <returns></returns>
        IEnumerable<GDIM_User> SaveUser(List<GDIM_User> usersToSave);

        /// <summary>
        /// to delete users
        /// </summary>
        /// <param name="usersToDelete"></param>
        /// <returns></returns>
        bool DeleteUsers(List<GDIM_User> usersToDelete);
    }
}
