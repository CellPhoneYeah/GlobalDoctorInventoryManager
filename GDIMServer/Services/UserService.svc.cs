using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessLayer.Entities;
using BusinessLayer;
using CommonUtility;
using ServerCommon;

namespace GDIMServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    public class UserService : IUserService
    {
        IBLUser blUser = null;
        public UserService()
        {
            try
            {
                blUser = PluginHelper.Instance.GetPluginValue<IBLUser>();
                if (blUser == null)
                    blUser = new BLUser();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GDIM_User> GetUsers(Func<GDIM_User,bool> conditions)
        {
            try
            {
                return blUser.GetUsers(conditions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GDIM_User> SaveUsers(List<GDIM_User> usersToSave)
        {
            try
            {
                return blUser.SaveUser(usersToSave);
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
                return blUser.DeleteUsers(usersToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
