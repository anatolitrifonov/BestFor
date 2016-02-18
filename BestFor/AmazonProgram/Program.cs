using System;
using AmazonProductAPI.Amazon.APAPI;

namespace AmazonProgram
{
    /// <summary>
    /// This program basically tests if connection to amazons still works at all.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Just run some search see if we get anything.

            // Instantiate Amazon ProductAdvertisingAPI client
            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = "Books";
            request.Title = "Fishing";
            request.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { request };
            itemSearch.AssociateTag = "madesimple0b-20";
            // itemSearch.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];

            // send the ItemSearch request
            // object v = amazonClient.ItemSearch(itemSearch);

            ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);
            

            // write out the results from the ItemSearch request
            foreach (var item in response.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.Title);
            }
            Console.WriteLine("done...enter any key to continue>");
            Console.ReadLine();
        }
    }
}
