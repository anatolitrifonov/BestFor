using BestFor.Common;
using BestFor.Dto.AffiliateProgram;
using BestFor.Services.Cache;
using BestFor.Services.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
    public class AmazonProductService : IProductService
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
        /// Injected cache manager.
        /// </summary>
        private ICacheManager _cacheManager;

        private int DEFAULT_PRODUCT_EXPIRATION_SECONDS = 300;
        // private string DEFAULT_SEARCH_INDEX = "Toys";
        private string DEFAULT_SEARCH_INDEX = "All";

        #region Classes

        Dictionary<string, string> _searchIndexes = new Dictionary<string, string>()
        {
            {"All", "All" },
            {"UnboxVideo", "Amazon Instant Video" },
            {"Appliances", "Appliances" },
            {"MobileApps", "Apps & Games" },
            {"ArtsAndCrafts", "Arts, Crafts & Sewing" },
            {"Automotive", "Automotive" },
            {"Baby", "Baby" },
            {"Beauty", "Beauty" },
            {"Books", "Books" },
            {"Music", "CDs & Vinyl" },
            {"Wireless", "Cell Phones & Accessories" },
            {"Fashion", "Clothing, Shoes & Jewelry" },
            {"FashionBaby", "Clothing, Shoes & Jewelry - Baby" },
            {"FashionBoys", "Clothing, Shoes & Jewelry - Boys" },
            {"FashionGirls", "Clothing, Shoes & Jewelry - Girls" },
            {"FashionMen", "Clothing, Shoes & Jewelry - Men" },
            {"FashionWomen", "Clothing, Shoes & Jewelry - Women" },
            {"Collectibles", "Collectibles & Fine Arts" },
            {"PCHardware", "Computers" },
            {"MP3Downloads", "Digital Music" },
            {"Electronics", "Electronics" },
            {"GiftCards", "Gift Cards" },
            {"Grocery", "Grocery & Gourmet Food" },
            {"HealthPersonalCare", "Health & Personal Care" },
            {"HomeGarden", "Home & Kitchen" },
            {"Industrial", "Industrial & Scientific" },
            {"KindleStore", "Kindle Store" },
            {"Luggage", "Luggage & Travel Gear" },
            {"Magazines", "Magazine Subscriptions" },
            {"Movies", "Movies & TV" },
            {"MusicalInstruments", "Musical Instruments" },
            {"OfficeProducts", "Office Products" },
            {"LawnAndGarden", "Patio, Lawn & Garden" },
            {"PetSupplies", "Pet Supplies" },
            {"Pantry", "Prime Pantry" },
            {"Software", "Software" },
            {"SportingGoods", "Sports & Outdoors" },
            {"Tools", "Tools & Home Improvement" },
            {"Toys", "Toys & Games" },
            {"VideoGames", "Video Games" },
            {"Wine", "Wine" },
            {"Blended", "Blended" }
        };

        private class ProductImage
        {
            public string Url { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
        #endregion

        /// <summary>
        /// Constructor. Nothing special. Might change later to accept a moregeneric class that allows configuration.
        /// </summary>
        /// <param name="accessKeyId"></param>
        /// <param name="accessSecret"></param>
        /// <param name="associateId"></param>
        public AmazonProductService(IOptions<AppSettings> appSettings, ICacheManager cacheManager)
        {
            _accessKeyId = appSettings.Value.AmazonAccessKeyId;
            _accessSecret = appSettings.Value.AmazonSecretKey;
            _associateId = appSettings.Value.AmazonAssociateId;
            _cacheManager = cacheManager;
        }

        #region IProductService implementation
        /// <summary>
        /// Parameters are read and filled by controller/caller.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public AffiliateProductDto FindProduct(ProductSearchParameters parameters)
        {
            // Check parameters. Do not throw expection if blank, just say that nothing found since this is an interface implementation. :)
            if (parameters == null || string.IsNullOrEmpty(parameters.Keyword) || string.IsNullOrWhiteSpace(parameters.Keyword)) return null;

            // Do some cleanup because amazon returns nothing on long searches.
            parameters.Keyword = CleanupKeywords(parameters.Keyword);
            parameters.Category = CleanupCategory(parameters.Category);

            // Generate product cache key
            var key = GetProductCacheKey(parameters);

            // First search cache
            var product = CheckCache(key);

            // Return if found
            if (product != null) return product;

            // Build call url
            var fullUrl = BuildProductSearchUrl(parameters);

            // Search for product. Takes time.
            product = CallProductSearch(fullUrl);

            // Would be strange that amazon did not find anything but ok.
            if (product == null) return null;

            // Cache product
            CacheProduct(key, product);

            return product;
        }
        #endregion

        /// <summary>
        /// Take only the first x words from the keywords
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public string CleanupKeywords(string keywords)
        {
            // This is not intended to be called from the outside and is public only for tests and transparency.
            // This is supposed to be called only from this class therefor we can throw an expection.
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrWhiteSpace(keywords))
                throw new Exception("AmazonProductService CleanupKeywords function is called with empty productKey.");

            var words = keywords.Split(' ');
            string result = "";
            // Take only four words.
            for (var i = 0; i < words.Length; i++)
            {
                result += words[i];
                if (i == words.Length - 1 || i == 3) return result;
                result += " ";
            }
            return result;
        }

        /// <summary>
        /// Make an actual web call to load products.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public AffiliateProductDto CallProductSearch(string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                throw new Exception("AmazonProductService CallProductSearch function is called with empty url.");

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                var task = client.GetAsync(new Uri(url));
                task.Wait();
                HttpResponseMessage response = task.Result;
                // response.EnsureSuccessStatusCode();
                var readTask = response.Content.ReadAsStringAsync();
                readTask.Wait();
                string result = readTask.Result;
                // try loading result into xml
                var reader = new StringReader(result);
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(result);
                System.Diagnostics.Debug.WriteLine(doc.ToString());
                return ReadXml(xmlDoc);
            }
        }

        /// <summary>
        /// Build the url for making rest call to amazon.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string BuildProductSearchUrl(ProductSearchParameters parameters)
        {
            // We are trying to build a URL like this.
            // http://webservices.amazon.com/onca/xml?Service=AWSECommerceService&Operation=ItemSearch&
            // AWSAccessKeyId =[Access Key ID] & AssociateTag =[Associate ID] & SearchIndex = Apparel &
            // Keywords = Shirt & Timestamp =[YYYY - MM - DDThh:mm: ssZ] & Signature =[Request Signature]

            // This is not intended to be called from the outside and is public only for tests and transparency.
            // This is supposed to be called only from this class therefor we can throw an expection.
            if (parameters == null)
                throw new Exception("AmazonProductService BuildProductSearchUrl function is called with empty ProductSearchParameters.");

            if (parameters.Keyword == null)
                throw new Exception("AmazonProductService BuildProductSearchUrl function is called with empty ProductSearchParameters.Keyword.");

            // Create timestamp
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // ResponseGroup
            // Specifies the types of values to return.You can specify multiple response groups in one request by separating them with commas.
            //Type: String
            //Default: Small
            //Valid Values: Accessories | BrowseNodes | EditorialReview | Images | ItemAttributes | ItemIds 
            // | Large | Medium | OfferFull | Offers | PromotionSummary | OfferSummary | RelatedItems | Reviews | 
            // SalesRank | Similarities | Small | Tracks | VariationImages | Variations(US only) | VariationSummary


            //                "&Keywords=" + parameters.Keyword + "&Operation=ItemSearch&ResponseGroup=Offers%2CItemAttributes" +
            // TODO add category selection to user settings or a random pick of category
            // Please for the love of God do not touch this line. I spent hours making this work.
            var url = "AWSAccessKeyId=" + _accessKeyId + "&AssociateTag=" + _associateId +
                "&Keywords=" + Uri.EscapeDataString(parameters.Keyword) +
                // "&Operation=ItemSearch&ResponseGroup=Offers%2CItemAttributes" +
                "&Operation=ItemSearch&ResponseGroup=ItemAttributes%2CImages%2CLarge%2CEditorialReview%2CItemIds%2CReviews" +
                "&SearchIndex=" + parameters.Category + "&Service=AWSECommerceService" +
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

            return fullUrl;

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
            // Lets parse all nodes under "Item"
            var products = new List<AffiliateProductDto>();
            for(var i = 0; i < nodes.Count; i++)
                products.Add(ParseProduct(nodes[i], nsmgr, namespacePrefix));
            // Pick the one that has an image and price
            foreach (var product in products)
                if (product.MiddleImageURL != null && !string.IsNullOrEmpty(product.FormattedPrice) &&
                    !string.IsNullOrWhiteSpace(product.FormattedPrice))
                    return product;
            // Pick the one that has an image and price
            foreach (var product in products)
                if (product.MiddleImageURL != null)
                    return product;
            // Nothing with the image -> pick the first one.
            return products[0];
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
            // This starts in Item node
            // check input
            if (productXmlNode == null) return null;

            var product = new AffiliateProductDto() { Merchant = "Amazon" };
            product.DetailPageURL = GetChildNodeValue(productXmlNode, "DetailPageURL");
            product.MerchantProductId = GetChildNodeValue(productXmlNode, "ASIN");

            var attributesNode = GetChildNode(productXmlNode, "ItemAttributes");
            product.Title = GetChildNodeValue(attributesNode, "Title");

            // Parse price
            var listPriceNode = GetChildNode(attributesNode, "ListPrice");
            // Parse list price node if there. Search for ItemAttributes/ListPrice/FormattedPrice
            if (listPriceNode != null)
            {
                product.FormattedPrice = GetChildNodeValue(listPriceNode, "FormattedPrice");
            }
            else
            {
                // Search for OfferSummary/LowestNewPrice/FormattedPrice
                var offerSummaryNode = GetChildNode(productXmlNode, "OfferSummary");
                if (offerSummaryNode != null)
                {
                    var lowestNewPriceNode = GetChildNode(offerSummaryNode, "LowestNewPrice");
                    if (lowestNewPriceNode != null)
                        product.FormattedPrice = GetChildNodeValue(lowestNewPriceNode, "FormattedPrice");
                }

            }

            // Parse description
            // Search for OfferSummary/LowestNewPrice/FormattedPrice
            var editorialReviewsNode = GetChildNode(productXmlNode, "EditorialReviews");
            if (editorialReviewsNode != null)
            {
                foreach(XmlNode editorialReviewNode in editorialReviewsNode.ChildNodes)
                {
                    if (editorialReviewNode.Name == "EditorialReview")
                    {
                        var source = GetChildNodeValue(editorialReviewNode, "Source");
                        var content = GetChildNodeValue(editorialReviewNode, "Content");
                        if (!product.Descriptions.ContainsKey(source))
                            product.Descriptions.Add(source, content);
                    }
                }
            }

            // Let's get all images because we need to pick some decent size.
            // I will let the function to add of not add image
            var images = new List<ProductImage>();
            // see if medium image is good enough
            ParseAndAddImage(images, productXmlNode, "MediumImage");
            // see if large image is good enough
            ParseAndAddImage(images, productXmlNode, "LargeImage");
            // see if small image is good enough
            ParseAndAddImage(images, productXmlNode, "SmallImage");

            if (images.Count > 0)
            {
                product.MiddleImageURL = images[0].Url;
                product.MiddleImageWidth = images[0].Width;
                product.MiddleImageHeight = images[0].Height;
            }

            return product;
        }

        /// <summary>
        /// Read image attributes from xml node and add to images list if goof enough
        /// </summary>
        /// <param name="images"></param>
        /// <param name="nsmgr"></param>
        /// <param name="namespacePrefix"></param>
        /// <param name="node"></param>
        private void ParseAndAddImage(List<ProductImage> images, XmlNode productNode, string imageNodeName)
        {
            // Basic image parcing.
            var image = new ProductImage();
            var imageNode = GetChildNode(productNode, imageNodeName);
            // exit if node not found
            if (imageNode == null) return;
            image.Url = GetChildNodeValue(imageNode, "URL");
            image.Width = GetChildNodeValueAsInt(imageNode, "Width");
            image.Height = GetChildNodeValueAsInt(imageNode, "Height");

            // Now let's see if this image is ok to add to the list.
            // If any conditions are met the image is not good enough to show
            if (image.Url == null || image.Url.Length < 10) return;
            // too small
            if (image.Width < 450 && image.Height < 450) return;
            // too large. Width or Height
            if (image.Width > 1000 || image.Height > 1000) return;
            // OK I guess.
            images.Add(image);

        }

        public string GetChildNodeValue(XmlNode node, string childName)
        {
            var child = GetChildNode(node, childName);
            if (child == null) return null;

            if (child.InnerText == null) return null; // not possible but need to check.

            return child.InnerText.Trim();
        }

        public XmlNode GetChildNode(XmlNode node, string childName)
        {
            // check input
            if (node == null || string.IsNullOrEmpty(childName) || string.IsNullOrWhiteSpace(childName)) return null;
            var children = node.ChildNodes;
            foreach (XmlNode child in children)
                if (child.Name == childName)
                    return child;
            return null;
        }

        public int GetChildNodeValueAsInt(XmlNode node, string childName)
        {
            string nodeValue = GetChildNodeValue(node, childName);
            // exit early if we can skip parsing
            if (nodeValue == null) return 0;
            int result;
            int.TryParse(nodeValue, out result);
            return result;
        }

        /// <summary>
        /// Look for the product in cache by key.
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        public AffiliateProductDto CheckCache(string productKey)
        {
            // This is not intended to be called from the outside and is public only for tests and transparency.
            // This is supposed to be called only from this class therefor we can throw an expection.
            if (string.IsNullOrEmpty(productKey) || string.IsNullOrWhiteSpace(productKey))
                throw new Exception("AmazonProductService CheckCache function is called with empty productKey.");
            if (_cacheManager == null) throw new Exception("AmazonProductService CheckCache function is called with empty cache manager.");
            // Check cache.
            var data = _cacheManager.Get(productKey);
            if (data == null) return null;
            // check the type
            var result = data as AffiliateProductDto;
            // If we could not cast then we have some sort of internal issue. Throw an exception.
            if (result == null) throw new Exception("AmazonProductService CheckCache function found object of the wrong type in cache.");
            return result;
        }

        /// <summary>
        /// Add product to cache.
        /// </summary>
        /// <param name="productKey"></param>
        /// <param name="product"></param>
        public void CacheProduct(string productKey, AffiliateProductDto product)
        {
            // This is not intended to be called from the outside and is public only for tests and transparency.
            // This is supposed to be called only from this class therefor we can throw an expection.
            if (string.IsNullOrEmpty(productKey) || string.IsNullOrWhiteSpace(productKey))
                throw new Exception("AmazonProductService CacheProduct function is called with empty productKey.");
            if (_cacheManager == null) throw new Exception("AmazonProductService CacheProduct function is called with empty cache manager.");
            // Re-Check cache in case product is already added while we were waiting for amazon.
            var data = _cacheManager.Get(productKey);
            // return if found
            if (data != null) return;
            // Add to cache
            _cacheManager.Add(productKey, product, DEFAULT_PRODUCT_EXPIRATION_SECONDS);
        }

        /// <summary>
        /// Genereates a key that will be used for caching products
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetProductCacheKey(ProductSearchParameters parameters)
        {
            // This is not intended to be called from the outside and is public only for tests and transparency.
            // This is supposed to be called only from this class therefor we can throw an expection.
            if (parameters == null)
                throw new Exception("AmazonProductService GetProductCacheKey function is called with empty ProductSearchParameters.");

            return CacheConstants.CACHE_KEY_PRODUCT_PREFIX + parameters.IndexKey;
        }

        /// <summary>
        /// Do some cleanup on the category.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public string CleanupCategory(string category)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(category)) return DEFAULT_SEARCH_INDEX;
            foreach(string indexKey in _searchIndexes.Keys)
            {
                if (indexKey.ToLower() == category.ToLower()) return indexKey;
            }
            return DEFAULT_SEARCH_INDEX;
        }
    }
}
