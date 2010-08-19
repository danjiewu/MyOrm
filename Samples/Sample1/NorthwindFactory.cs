using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
	public class NorthwindFactory
	{
        public static IServiceFactory ServiceFactory = RemoteServiceFactory.GenerateRemoteHttpHandleFactory(@"http://localhost:6710/Service/HttpInvoke.asmx");
	}
}
