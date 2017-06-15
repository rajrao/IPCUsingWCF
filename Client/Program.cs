using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hit enter to send message. e to exit");
			using (ServiceProxy serviceProxy = new ServiceProxy())
			{
				do
				{
					var c = Console.ReadLine();
					if (c == "e")
					{
						break;
					}
					serviceProxy.ProcessCommandLineArguments(new[] {"Sending a message at", DateTime.Now.ToString()});
				} while (true);
			}
		}
	}
}
