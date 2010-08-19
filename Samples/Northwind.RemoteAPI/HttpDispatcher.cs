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
                serializer.Serialize(ms, method.Name);
                serializer.Serialize(ms, method.MetadataToken);
                serializer.Serialize(ms, args);

                request.ReadWriteTimeout = 10000;
                request.ContentLength = ms.Length;

                using (Stream inputStream = request.GetRequestStream())
                {
                    ms.WriteTo(inputStream);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream outputStream = response.GetResponseStream())
                {
                    object[] ret = (object[])serializer.Deserialize(outputStream);
                    bool success = (bool)ret[0];
                    if (success)
                        return ret[1];
                    else
                        throw (Exception)ret[1];
                }
            }
        }
        #endregion
    }
}
