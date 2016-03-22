﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto.AffiliateProgram
{
    public class AffiliateProductDto : ErrorMessageDto
    {
        /// <summary>
        /// Name of the merchant the prodcut is from. Example: Amazon.
        /// </summary>
        public string Merchant { get; set; }

        /// <summary>
        /// ProductId is the merchan's system.
        /// </summary>
        public string MerchantProductId { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Title { get; set; }

        public string CurrencyCode { get; set; }

        /// <summary>
        /// Price to display
        /// </summary>
        public string FormattedPrice { get; set; }

        /// <summary>
        /// Price converted to number
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Link to item details
        /// </summary>
        public string DetailPageURL { get; set; }

        /// <summary>
        /// Middle image URL
        /// </summary>
        public string MiddleImageURL { get; set; }

        public int MiddleImageWidth { get; set; }

        public int MiddleImageHeight { get; set; }


    }
}
