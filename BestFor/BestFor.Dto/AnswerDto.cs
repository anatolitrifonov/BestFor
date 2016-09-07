using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents a simple words suggestion for typeahead text box.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerDto : BaseDto
    {
        public class AnswerCategory
        {
            public string Code { get; set; }

            public string Name { get; set; }
        }

        public AnswerDto()
        {
            this.Categories = new List<AnswerCategory>();
            this.Categories.Add(new AnswerCategory() { Code = "Baby", Name = "Baby" });
            this.Categories.Add(new AnswerCategory() { Code = "Beauty", Name = "Beauty" });
            this.Categories.Add(new AnswerCategory() { Code = "Books", Name = "Books" });
            this.Categories.Add(new AnswerCategory() { Code = "Electronics", Name = "Electronics" });
            this.Categories.Add(new AnswerCategory() { Code = "Fashion", Name = "Fashion" });
            this.Categories.Add(new AnswerCategory() { Code = "Movies", Name = "Movies" });
            this.Categories.Add(new AnswerCategory() { Code = "Toys", Name = "Toys" });
            this.Categories.Add(new AnswerCategory() { Code = "VideoGames", Name = "Video Games" });
            this.Categories.Add(new AnswerCategory() { Code = "SportingGoods", Name = "Sporting Goods" });
            this.Categories.Add(new AnswerCategory() { Code = "OfficeProducts", Name = "Office Products" });
            this.Categories.Add(new AnswerCategory() { Code = "HealthPersonalCare", Name = "Health & Personal Care" });

            //{ "All", "All" },
            //{ "UnboxVideo", "Amazon Instant Video" },
            //{ "Appliances", "Appliances" },
            //{ "MobileApps", "Apps & Games" },
            //{ "ArtsAndCrafts", "Arts, Crafts & Sewing" },
            //{ "Automotive", "Automotive" },
            //{ "Baby", "Baby" },
            //{ "Beauty", "Beauty" },
            //{ "Books", "Books" },
            //{ "Music", "CDs & Vinyl" },
            //{ "Wireless", "Cell Phones & Accessories" },
            //{ "Fashion", "Clothing, Shoes & Jewelry" },
            //{ "FashionBaby", "Clothing, Shoes & Jewelry - Baby" },
            //{ "FashionBoys", "Clothing, Shoes & Jewelry - Boys" },
            //{ "FashionGirls", "Clothing, Shoes & Jewelry - Girls" },
            //{ "FashionMen", "Clothing, Shoes & Jewelry - Men" },
            //{ "FashionWomen", "Clothing, Shoes & Jewelry - Women" },
            //{ "Collectibles", "Collectibles & Fine Arts" },
            //{ "PCHardware", "Computers" },
            //{ "MP3Downloads", "Digital Music" },
            //{ "Electronics", "Electronics" },
            //{ "GiftCards", "Gift Cards" },
            //{ "Grocery", "Grocery & Gourmet Food" },
            //{ "HealthPersonalCare", "Health & Personal Care" },
            //{ "HomeGarden", "Home & Kitchen" },
            //{ "Industrial", "Industrial & Scientific" },
            //{ "KindleStore", "Kindle Store" },
            //{ "Luggage", "Luggage & Travel Gear" },
            //{ "Magazines", "Magazine Subscriptions" },
            //{ "Movies", "Movies & TV" },
            //{ "MusicalInstruments", "Musical Instruments" },
            //{ "OfficeProducts", "Office Products" },
            //{ "LawnAndGarden", "Patio, Lawn & Garden" },
            //{ "PetSupplies", "Pet Supplies" },
            //{ "Pantry", "Prime Pantry" },
            //{ "Software", "Software" },
            //{ "SportingGoods", "Sports & Outdoors" },
            //{ "Tools", "Tools & Home Improvement" },
            //{ "Toys", "Toys & Games" },
            //{ "VideoGames", "Video Games" },
            //{ "Wine", "Wine" },
            //{ "Blended", "Blended" }
        }

        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// Usually = searchindex name at least for amazon
        /// </summary>
        public string Category { get; set; }

        public readonly List<AnswerCategory> Categories = new List<AnswerCategory>();
    }
}
