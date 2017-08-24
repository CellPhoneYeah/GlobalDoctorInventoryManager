using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Common
{
    public class WcfChannelFactory
    {
        public WcfChannelFactory()
        {

        }

        /// <summary>
        /// execute remote method WSHttpBinding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T GetRemoteInstance<T>(BindingTypes bindingType,string uri)
        {
            Binding binding = GetBinding(bindingType);
            EndpointAddress endpoint = new EndpointAddress(uri);
            using (ChannelFactory<T> channelFactory = new ChannelFactory<T>(binding,endpoint))
            {
                try
                {
                    T instance = channelFactory.CreateChannel();
                    return instance;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static Binding GetBinding(BindingTypes bindingType)
        {
            Binding bindingInstance = null;
            switch (bindingType)
            {
                case BindingTypes.Basic:
                    BasicHttpBinding basic = new BasicHttpBinding();
                    basic.MaxBufferSize = 2147483647;
                    basic.MaxBufferPoolSize = 2147483647;
                    basic.MaxReceivedMessageSize = 2147483647;
                    //basic.ReaderQuotas.MaxStringContentLength = 2147483647;
                    basic.CloseTimeout = new TimeSpan(0, 30, 0);
                    basic.OpenTimeout = new TimeSpan(0, 30, 0);
                    basic.ReceiveTimeout = new TimeSpan(0, 30, 0);
                    basic.SendTimeout = new TimeSpan(0, 30, 0);
                    bindingInstance = basic;
                    break;
                case BindingTypes.Net:
                    NetTcpBinding net = new NetTcpBinding();
                    net.MaxReceivedMessageSize = 65535000;
                    net.Security.Mode = SecurityMode.None;
                    bindingInstance = net;
                    break;
                case BindingTypes.WS:
                    WSHttpBinding ws = new WSHttpBinding(SecurityMode.None);
                    ws.MaxReceivedMessageSize = 65535000;
                    ws.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;
                    ws.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Windows;
                    bindingInstance = ws;
                    break;
                default:
                    break;
            }
            return bindingInstance;
        }
    }

    public enum BindingTypes
    {
        Basic =0,
        WS ,
        Net
    }
}
