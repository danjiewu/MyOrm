using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Northwind.Data;
using Northwind;

namespace SampleService
{
    /// <summary>
    /// RemoteDAOService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://northwind.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class RemoteDAOService : System.Web.Services.WebService
    {

        [WebMethod]
        public byte[] SerializedRemoteInvoke(byte[] serializedArgs)
        {
            using (MemoryStream ms = new MemoryStream(serializedArgs))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                MethodInfo method = (MethodInfo)serializer.Deserialize(ms);
                object[] args = (object[])serializer.Deserialize(ms);
                foreach (PropertyInfo property in typeof(IDAOFactory).GetProperties())
                {
                    if (method.DeclaringType.IsAssignableFrom(property.PropertyType))
                    {
                        using (MemoryStream valueStream = new MemoryStream())
                        {
                            serializer.Serialize(valueStream, method.Invoke(property.GetValue(NorthwindFactory.DAOFactory, null), args));
                            return valueStream.ToArray();
                        }
                    }
                }
                return null;
            }
        }
    }
}
