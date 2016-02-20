using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using BestFor.Dto.AffiliateProgram;

namespace BestFor.Services.AffiliateProgram.Amazon
{
    /// <summary>
    /// Grand schema of things. Use answer as keyword to search amazon for related product. We will make it smart later.
    /// Smart is in we will somehow map the answer to amazon search index.
    /// For now we will just pick some index and find some products in it based on answer.
    /// Then we will render the product on the answer page.
    /// Hopefully link will be interesting enough for someone to click on it.
    /// Then may be they will even buy something and we will not have our efforts wasted:)
    /// </summary>
    /// <remarks>
    /// Documentation http://docs.aws.amazon.com/AWSECommerceService/latest/DG/AnatomyOfaRESTRequest.html
    /// </remarks>
    public class AmazonProductService
    {
        /// <summary>
        /// This is a access "known" key 
        /// </summary>
        private string _accessKeyId;
        /// <summary>
        /// This is a super secret key. It is a very bad idea to have it in config file.
        /// </summary>
        private string _accessSecret;
        /// <summary>
        /// This is our associate id.
        /// </summary>
        private string _associateId;

        /// <summary>
        /// Constructor. Nothing special. Might change later to accept a moregeneric class that allows configuration.
        /// </summary>
        /// <param name="accessKeyId"></param>
        /// <param name="accessSecret"></param>
        /// <param name="associateId"></param>
        public AmazonProductService(string accessKeyId, string accessSecret, string associateId)
        {
            _accessKeyId = accessKeyId;
            _accessSecret = accessSecret;
            _associateId = associateId;
        }

        public AffiliateProductDto FindProduct(string keyword)
        {
            // We are trying to build a URL like this.
            // http://webservices.amazon.com/onca/xml?Service=AWSECommerceService&Operation=ItemSearch&
            // AWSAccessKeyId =[Access Key ID] & AssociateTag =[Associate ID] & SearchIndex = Apparel &
            // Keywords = Shirt & Timestamp =[YYYY - MM - DDThh:mm: ssZ] & Signature =[Request Signature]

            // Create timestamp
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // Please for the love of God do not touch this line. I spent hours making this work.
            var url = "AWSAccessKeyId=" + _accessKeyId  + "&AssociateTag=" + _associateId +
                "&Keywords=" + keyword + "&Operation=ItemSearch&ResponseGroup=Offers%2CItemAttributes" +
                "&SearchIndex=Books&Service=AWSECommerceService" +
                "&Timestamp=" + Uri.EscapeDataString(timeStamp) + "&Version=2013-08-01";

            // Business of signing amazon request. Not a good idea to touch this.
            var dataToSign = "GET\n" +
                "webservices.amazon.com\n" +
                "/onca/xml\n" + url;
            var bytesToSign = Encoding.UTF8.GetBytes(dataToSign);
            var secretKeyBytes = Encoding.UTF8.GetBytes(_accessSecret);
            var hmacSha256 = new HMACSHA256(secretKeyBytes);
            var hashBytes = hmacSha256.ComputeHash(bytesToSign);
            var signature = Convert.ToBase64String(hashBytes);
            var fullUrl = "http://webservices.amazon.com/onca/xml?" + url + "&Signature=" + Uri.EscapeDataString(signature);

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                HttpResponseMessage response = client.GetAsync(new Uri(fullUrl)).Result;
                // response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                // try loading result into xml
                var reader = new StringReader(result);
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
                return ReadXml(xmlDoc);
            }
        }

        /// <summary>
        /// Parses xml returned by amazon and returns the first product from the list.
        /// Xml is loaded into XmlDocument
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public AffiliateProductDto ReadXml(XmlDocument xmlDoc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            var namespacePrefix = "ab"; // This could be anything. But I since we do not want to to have quoated strings flying around
            // I'd rather pass it as parameter.
            nsmgr.AddNamespace(namespacePrefix, "http://webservices.amazon.com/AWSECommerceService/2013-08-01");
            var nodes = xmlDoc.GetElementsByTagName("Item");
            // I doubt this can ever be null but we wil check anyway
            if (nodes == null || nodes.Count == 0) return null;
            // Take the first one.
            return ParseProduct(nodes[0], nsmgr, namespacePrefix);
        }

        /// <summary>
        /// Parses amazon product details into a generic dto object from the xml node retuned by amazon rest call.
        /// </summary>
        /// <param name="productXmlNode"></param>
        /// <returns></returns>
        /// <remarks>
        /// Check the myxml.xml file for xml example.
        /// I specifically decided not to parse into an object. Too much trouble with serialization/deserialization.
        /// Underneath it is probably parsing each node anyway when converting to object.
        /// </remarks>
        public AffiliateProductDto ParseProduct(XmlNode productXmlNode, XmlNamespaceManager nsmgr, string namespacePrefix)
        {
            // check input
            if (productXmlNode == null) return null;

            var product = new AffiliateProductDto() { Merchant = "Amazon" };
            product.DetailPageURL = GetNodeChildValue(nsmgr, namespacePrefix, productXmlNode, "DetailPageURL");
            product.Title = GetNodeChildValue(nsmgr, namespacePrefix, productXmlNode, "Title");
            product.MerchantProductId = GetNodeChildValue(nsmgr, namespacePrefix, productXmlNode, "ASIN");
            return product;
        }

        /// <summary>
        /// Search node's immediate children for name and return the inner text.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetNodeChildValue(XmlNamespaceManager nsmgr, string namespacePrefix, XmlNode node, string childName)
        {
            // check input
            if (node == null || string.IsNullOrEmpty(childName) || string.IsNullOrWhiteSpace(childName)) return null;
            // search for child by name
            // Example path to the immediate child is "//ab:Title"
            var child = node.SelectSingleNode("//" + namespacePrefix + ":" + childName, nsmgr);
            if (child == null) return null;
            return child.InnerText;
        }
    }
}
