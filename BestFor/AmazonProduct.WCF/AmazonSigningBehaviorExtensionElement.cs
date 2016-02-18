using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace AmazonProduct.WCF
{
    /// <summary>
    /// Represents a configuration element that contains sub-elements that specify
    /// behavior extensions, which enable the user to customize service or endpoint
    /// behaviors.
    /// </summary>
    /// <remarks>This is how we are customizing request going to Amazon.
    /// "Customizaton" consists of passing our Amazon account info in SOAP request headers.
    /// This class hooks us up into config file giving ability to read config values from it and setting them into request header
    /// </remarks>
    public class AmazonSigningBehaviorExtensionElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Class is instantiated when when instance of web service client in created
        /// </summary>
        public AmazonSigningBehaviorExtensionElement()
        {
        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <remarks>Called when instance of web service client in created. Do not know what for.</remarks>
        public override Type BehaviorType
        {
            get
            {
                return typeof(AmazonSigningEndpointBehavior);
            }
        }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings
        /// </summary>
        /// <remarks>Called when instance of web service client in created.</remarks>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new AmazonSigningEndpointBehavior(AmazonAccessKeyId, AmazonSecretKey, AmazonSecurityNamespace);
        }

        [ConfigurationProperty("AmazonAccessKeyId", IsRequired = true)]
        public string AmazonAccessKeyId
        {
            get { return (string)base["AmazonAccessKeyId"]; }
            set { base["AmazonAccessKeyId"] = value; }
        }

        [ConfigurationProperty("AmazonSecretKey", IsRequired = true)]
        public string AmazonSecretKey
        {
            get { return (string)base["AmazonSecretKey"]; }
            set { base["AmazonSecretKey"] = value; }
        }

        [ConfigurationProperty("AmazonSecurityNamespace", IsRequired = true)]
        public string AmazonSecurityNamespace
        {
            get { return (string)base["AmazonSecurityNamespace"]; }
            set { base["AmazonSecurityNamespace"] = value; }
        }
    }
}
