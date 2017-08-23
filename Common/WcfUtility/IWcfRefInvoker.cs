using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Common
{
    public interface IWcfRefInvoker
    {
        Binding GetBinding();
        
        T CreateWCFServiceByUrl<T>(string url,string binding)
    }
}
