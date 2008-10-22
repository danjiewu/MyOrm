using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Northwind.Data;
using Northwind;

namespace SampleService
{
    public class RemoteDAOHandler : IHttpHandler
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
                string daoName = (string)serializer.Deserialize(stream);
                MethodInfo method = (MethodInfo)serializer.Deserialize(stream);
                object[] args = (object[])serializer.Deserialize(stream);
                try
                {
                    object ret = method.Invoke(typeof(IDAOFactory).GetProperty(daoName).GetValue(NorthwindFactory.DAOFactory, null), args);
                    serializer.Serialize(context.Response.OutputStream, true);
                    serializer.Serialize(context.Response.OutputStream, ret);
                }
                catch (Exception e)
                {
                    serializer.Serialize(context.Response.OutputStream, false);
                    serializer.Serialize(context.Response.OutputStream, e);
                }
            }
        }

        #endregion
    }
}
