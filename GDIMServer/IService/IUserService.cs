using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GDIMServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        IEnumerable<GDIM_User> GetUsers(Func<GDIM_User,bool> conditions);

        [OperationContract]
        IEnumerable<GDIM_User> SaveUsers(List<GDIM_User> usersToSave);

        [OperationContract]
        bool DeleteUsers(List<GDIM_User> usersToDelete);
    }
}
