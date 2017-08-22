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
        public static object ExecuteMethodWS<T>(string uri, string methodName, params object[] args)
        {
            WSHttpBinding binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            EndpointAddress endpoint = new EndpointAddress(uri);
            using (ChannelFactory<T> channelFactory = new ChannelFactory<T>(binding,endpoint))
            {
                T instance = channelFactory.CreateChannel();
                using (instance as IDisposable)
                {
                    try
                    {
                        Type type = typeof(T);
                        MethodInfo methodInfo = type.GetMethod(methodName);
                        return methodInfo.Invoke(instance, args);
                    }
                    catch (TimeoutException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (CommunicationException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (Exception)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// execute remote method NetTcpBinding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object ExecuteMethodNTB<T>(string uri, string methodName, params object[] args)
        {
            EndpointAddress address = new EndpointAddress(uri);
            Binding bindingInstance = null;
            NetTcpBinding ntb = new NetTcpBinding();
            ntb.MaxReceivedMessageSize = 20971520;
            ntb.Security.Mode = SecurityMode.None;
            bindingInstance = ntb;
            using (ChannelFactory<T> channelFactory = new ChannelFactory<T>(bindingInstance,address))
            {
                T instance = channelFactory.CreateChannel();
                using (instance as IDisposable)
                {
                    try
                    {
                        Type type = typeof(T);
                        MethodInfo methodInfo = type.GetMethod(methodName);
                        return methodInfo.Invoke(instance, args);
                    }
                    catch (TimeoutException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (CommunicationException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (Exception)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                }
            }
        }
    }
}
