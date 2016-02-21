﻿using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;

namespace BestFor.Dto.Account
{
    public class SendCodeViewDto
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}