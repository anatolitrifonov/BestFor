using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.RegularExpressions;

using System.IO;
using System.Xml;

namespace AmazonProduct.WCF
{
	public class AmazonSigningMessageInspector : IClientMessageInspector {
        private string _accessKeyId = "";
        private string _secretKey = "";
        private string _securityNameSpace = "";


        public AmazonSigningMessageInspector(string accessKeyId, string secretKey, string securityNameSpace)
        {
            _accessKeyId = accessKeyId;
            _secretKey = secretKey;
            _securityNameSpace = securityNameSpace;
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel) {
			// prepare the data to sign
			string		operation		= Regex.Match(request.Headers.Action, "[^/]+$").ToString();
			DateTime	now				= DateTime.UtcNow;
			string		timestamp		= now.ToString("yyyy-MM-ddTHH:mm:ssZ");
			string		signMe			= operation + timestamp;
			byte[]		bytesToSign		= Encoding.UTF8.GetBytes(signMe);

			// sign the data
			byte[]		secretKeyBytes	= Encoding.UTF8.GetBytes(_secretKey);
			HMAC		hmacSha256		= new HMACSHA256(secretKeyBytes);
			byte[]		hashBytes		= hmacSha256.ComputeHash(bytesToSign);
			string		signature		= Convert.ToBase64String(hashBytes);

			// add the signature information to the request headers
            request.Headers.Add(new AmazonHeader("AWSAccessKeyId", _accessKeyId, _securityNameSpace));
            request.Headers.Add(new AmazonHeader("Timestamp", timestamp, _securityNameSpace));
            request.Headers.Add(new AmazonHeader("Signature", signature, _securityNameSpace));

			return null;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState)

        {
            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            Message message = buffer.CreateMessage();

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            message.WriteMessage(xmlTextWriter);
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
            String messageContent = stringWriter.ToString();
            System.Diagnostics.Debug.WriteLine(messageContent);

            //Assign a copy to the ref received
            reply = buffer.CreateMessage();
        }
	}
}
