using System.ServiceModel;

namespace Common
{
	[ServiceContract]
	public interface IApplicationInterprocessCommunicationService
	{
		[OperationContract(IsOneWay = true)]
		void ProcessCommandLineArguments(string[] arguments);
	}
}