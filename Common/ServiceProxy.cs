using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class ServiceProxy : ClientBase<IApplicationInterprocessCommunicationService>
	{
		public ServiceProxy()
			: base(new ServiceEndpoint(ContractDescription.GetContract(typeof(IApplicationInterprocessCommunicationService)),
				new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/IApplicationInterprocessCommunicationService")))
		{

		}
		public void ProcessCommandLineArguments(string[] arguments)
		{
			Channel.ProcessCommandLineArguments(arguments);
		}
	}
}
