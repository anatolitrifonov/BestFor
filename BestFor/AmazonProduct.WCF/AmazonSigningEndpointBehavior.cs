using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace AmazonProduct.WCF
{
	public class AmazonSigningEndpointBehavior : IEndpointBehavior {
		private string _accessKeyId = "";
		private string _secretKey = "";
        private string _securityNameSpace = "";

		public AmazonSigningEndpointBehavior(string accessKeyId, string secretKey, string securityNameSpace) {
			_accessKeyId = accessKeyId;
			_secretKey = secretKey;
            _securityNameSpace = securityNameSpace;
        }

        /// <summary>
        /// Part of IEndpointBehavior. Implements a modification or extension of the client across an endpoint.
        /// 
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        /// <param name="clientRuntime"></param>
		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime) {
            clientRuntime.MessageInspectors.Add(new AmazonSigningMessageInspector(_accessKeyId, _secretKey, _securityNameSpace));
		}

        /// <summary>
        /// Part of IEndpointBehavior. Implements a modification or extension of the service across an endpoint.
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        /// <param name="endpointDispatcher"></param>
		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher) { return; }

        /// <summary>
        /// Part of IEndpointBehavior. Implement to confirm that the endpoint meets some intended criteria.
        /// </summary>
        /// <param name="serviceEndpoint"></param>
		public void Validate(ServiceEndpoint serviceEndpoint) { return; }

        /// <summary>
        /// Part of IEndpointBehavior. Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        /// <param name="bindingParameters"></param>
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters) { return; }
	}
}
