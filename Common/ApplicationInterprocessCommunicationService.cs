using System;

namespace Common
{
	public class ApplicationInterprocessCommunicationService : IApplicationInterprocessCommunicationService
	{
		public void ProcessCommandLineArguments(string[] arguments)
		{
			Console.WriteLine("Messages received!");
			foreach (string argument in arguments)
			{
				Console.WriteLine(argument);
			}
		}
	}
}