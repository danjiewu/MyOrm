using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace Northwind.Protocal
{
    public class HttpDispatcher : IRemoteDispatcher
    {
        private static ILog logger = LogManager.GetLogger("HttpDispatcher");
        #region IRemoteDispatcher Members

        public HttpDispatcher(string serviceUrl)
        {
            ServiceUrl = serviceUrl;
        }

        public string ServiceUrl
        {
            get;
            private set;
        }

        public object RemoteInvoke(string serviceName, MethodInfo method, object[] args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceUrl);
            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, serviceName);
                serializer.Serialize(ms, method);
                serializer.Serialize(ms, args);

                request.ReadWriteTimeout = 10000;
                request.ContentLength = ms.Length;

                string strArgs = String.Join(",", Array.ConvertAll<object, string>(args, delegate(object o) { return o == null ? "null" : Convert.ToString(o); }));
                logger.DebugFormat("DAO:{{{0}}} Method:{{{1}}} invoked, Args:{{{2}}}. {3} bytes to send.", serviceName, method.Name, strArgs, ms.Length);

                using (Stream inputStream = request.GetRequestStream())
                {
                    ms.WriteTo(inputStream);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream outputStream = response.GetResponseStream())
                {

                    bool success = (bool)serializer.Deserialize(outputStream);
                    object ret = serializer.Deserialize(outputStream);
                    try
                    {
                        if (success)
                            return ret;
                        else
                            throw (Exception)ret;
                    }
                    finally
                    {
                        logger.DebugFormat("DAO:{{{0}}} Method:{{{1}}} finished. Returns:{{{2}}}. {3} bytes recieved.", serviceName, method.Name, ret, response.ContentLength);
                    }

                }
            }
        }
        #endregion
    }    
}
