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

namespace BestFor.Services.AffiliateProgram.Amazon
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Documentation http://docs.aws.amazon.com/AWSECommerceService/latest/DG/AnatomyOfaRESTRequest.html
    /// </remarks>
    public class AmazonProductService
    {
        private string _accessKeyId;
        private string _accessSecret;
        private string _associateId;
        public AmazonProductService(string accessKeyId, string accessSecret, string associateId)
        {
            _accessKeyId = accessKeyId;
            _accessSecret = accessSecret;
            _associateId = associateId;
        }

        public void FindProduct(string keyword)
        {
            // http://webservices.amazon.com/onca/xml?Service=AWSECommerceService&Operation=ItemSearch&
            // AWSAccessKeyId =[Access Key ID] & AssociateTag =[Associate ID] & SearchIndex = Apparel &
            // Keywords = Shirt & Timestamp =[YYYY - MM - DDThh:mm: ssZ] & Signature =[Request Signature]

            // Create timestamp
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var url = "AWSAccessKeyId=" + _accessKeyId  + "&AssociateTag=" + _associateId +
                "&Keywords=" + keyword + "&Operation=ItemSearch&ResponseGroup=Offers%2CItemAttributes" +
                "&SearchIndex=Books&Service=AWSECommerceService" +
                "&Timestamp=" + Uri.EscapeDataString(timeStamp) + "&Version=2013-08-01";

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
                //var doc = new XmlDocument();
                //doc.Load(reader);
                //doc.Save("C:\\Temp\\myxml.xml");

                //AmazonProductAPI.Amazon.APAPI.ItemSearchResponse myObject;
                //// Construct an instance of the XmlSerializer with the type
                //// of object that is being deserialized.
                //XmlSerializer mySerializer =
                //new XmlSerializer(typeof(ItemSearchResponse));
                ////// To read the file, create a FileStream.
                ////FileStream myFileStream =
                ////new FileStream("myFileName.xml", FileMode.Open);
                //// Call the Deserialize method and cast to the object type.
                //myObject = (AmazonProductAPI.Amazon.APAPI.ItemSearchResponse)mySerializer.Deserialize(reader);

                //string h = "dfsf";
            }
        }

        public void ReadXml()
        {
            FileStream myFileStream = new FileStream("C:\\Users\\atrifono\\Documents\\Personal\\Fork\\BestFor\\src\\BestFor.Services\\AffiliateProgram\\Amazon\\myxml.xml", FileMode.Open);
            var doc = new XmlDocument();
            doc.Load(myFileStream);
            var node = doc.GetElementsByTagName("Item")[0];
            string data = node.InnerXml;
            string h = "df";

        }
    }
}
