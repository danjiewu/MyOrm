﻿using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Northwind.Business;

namespace Northwind.Web.Service
{
    /// <summary>
    /// ServiceHttpHandler 的摘要说明
    /// </summary>
    public class ServiceHttpHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            using (Stream stream = context.Request.InputStream)
            {
                BinaryFormatter serializer = new BinaryFormatter();
                string serviceName = (string)serializer.Deserialize(stream);
                string methodName = (string)serializer.Deserialize(stream);
                int metadataToken = (int)serializer.Deserialize(stream);
                object[] args = (object[])serializer.Deserialize(stream);
                MethodInfo method = (MethodInfo)typeof(IEntityService).Module.ResolveMethod(metadataToken);
                try
                {
                    object ret = method.Invoke(typeof(IServiceFactory).GetProperty(serviceName).GetValue(NorthwindFactory.ServiceFactory, null), args);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        serializer.Serialize(ms, new object[] { true, ret });
                        ms.WriteTo(context.Response.OutputStream);
                    }
                }
                catch (Exception e)
                {
                    while (e is TargetInvocationException)
                        e = e.InnerException;
                    context.Response.ClearContent();
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            serializer.Serialize(ms, new object[] { false, e });
                            ms.WriteTo(context.Response.OutputStream);
                        }
                    }
                    catch
                    {
                        context.Response.ClearContent();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            serializer.Serialize(ms, new object[] { false, new Exception(e.Message) });
                            ms.WriteTo(context.Response.OutputStream);
                        }
                    }
                }
            }
        }

        #endregion
    }
}