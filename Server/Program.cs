using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Win32;

namespace ConsoleApplication1
{
	

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Setting up registry");
			UriProtocolHandler uriProtocolHandler = new UriProtocolHandler();
			var path = Assembly.GetExecutingAssembly().Location;
			uriProtocolHandler.CreateUriProtocolHandler(path);
			Console.WriteLine("Setting up registry");


			Console.WriteLine("Initializing Ping");
			using (var p = new Ping())
			{
				Console.WriteLine("Sending Ping");
				PingReply pingReply = p.Send("es1sbadmin.corporate.ncm",1000);
				if (pingReply != null && pingReply.Status == IPStatus.Success)
				{
					Console.WriteLine("Success");
				}
				else
				{
					Console.WriteLine("Failed");
				}
			}
			Console.WriteLine("Setting up IPC");
			Task.Run(() =>
			{
				using (
					ServiceHost serviceHost = new ServiceHost(typeof(ApplicationInterprocessCommunicationService),
						new Uri("net.pipe://localhost/")))
				{
					serviceHost.AddServiceEndpoint(typeof(IApplicationInterprocessCommunicationService), new NetNamedPipeBinding(),
						"IApplicationInterprocessCommunicationService");
					serviceHost.Open();
					Console.WriteLine("IPC channel opened");
					AutoResetEvent autoEvent = new AutoResetEvent(false);
					autoEvent.WaitOne();

					Console.WriteLine("IPC channel closing...");
					serviceHost.Close();
					Console.WriteLine("IPC channel closed");
				}
			});

			Console.WriteLine("Done with IPC");
			Console.WriteLine("Done! Hit enter to exit");
			Console.Read();
		}
	}

	
}
