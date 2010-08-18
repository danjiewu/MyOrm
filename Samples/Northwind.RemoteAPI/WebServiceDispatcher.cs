using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Reflection;

namespace Northwind.Protocal
{
    public class WebServiceDispatcher : IRemoteDispatcher
    {
        private static ILog logger = LogManager.GetLogger("WebServiceDispatcher");
        #region IRemoteDispatcher Members
        private RemoteService remoteService = new RemoteService();

        public WebServiceDispatcher(string serviceUrl)
        {
            remoteService.Url = serviceUrl;
        }

        public string ServiceUrl
        {
            get { return remoteService.Url; }
        }

        public object RemoteInvoke(string serviceName, MethodInfo method, object[] args)
        {            
            return remoteService.InvokeService(serviceName, method.Name, args);
        }

        #endregion
    }
}
