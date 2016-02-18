using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Xml;

namespace AmazonProduct.WCF
{
    /// <summary>
    /// Extends the content of a SOAP header with additional value that needs to be sent to Amazon.
    /// Message inspector adds this additional header to each Amazon SOAP request
    /// </summary>
	public class AmazonHeader : MessageHeader 
    {
		private string _name;
		private string _value;
        private string _securityNameSpace;

		public AmazonHeader(string name, string value, string securityNameSpace) {
			_name = name;
			_value = value;
            _securityNameSpace = securityNameSpace;
		}

        /// <summary>
        /// Called when SOAP request is built
        /// </summary>
		public override string Name
        {
            get
            {
                return _name;
            }
        } 

        /// <summary>
        /// Message inspector calls this property when adding header to request in BeforeSendRequest
        /// </summary>
		public override string Namespace
        {
            get
            {
                return _securityNameSpace; 
            }
        }


        /// <summary>
        /// 
        /// Called when the header content is serialized using the specified XML writer.
        /// This is the only abstract method of MessageHeaderInfo and it has to be overrwritten 
        /// </summary>
        /// <param name="xmlDictionaryWriter"></param>
        /// <param name="messageVersion"></param>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter xmlDictionaryWriter, MessageVersion messageVersion)
        {
            // Add the header value to request header.
			xmlDictionaryWriter.WriteString(_value);
		}
	}
}
