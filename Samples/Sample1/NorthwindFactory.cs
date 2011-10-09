using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
	public class NorthwindFactory
	{
        public static IServiceFactory ServiceFactory = RemoteServiceFactory.GenerateRemoteHttpHandleFactory(@"http://localhost/Northwind/Service/ServiceHttpHandler.ashx");
		//public static IServiceFactory ServiceFactory = RemoteServiceFactory.GenerateRemoteWebServiceFactory(@"http://localhost/Northwind/Service/RemoteService.asmx");
	}
}
