using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
	public class NorthwindFactory
	{
        public static IServiceFactory ServiceFactory = RemoteServiceFactory.GenerateRemoteWebServiceFactory(@"http://localhost:6710/Service/RemoteService.asmx");
	}
}
