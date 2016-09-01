using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Models
{
    /// <summary>
    /// Model for Error page
    /// </summary>
    public class ErrorViewModel
    {

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public void AddError(string error)
        {
            if (string.IsNullOrEmpty(error) || string.IsNullOrWhiteSpace(error))
                throw new Exception("invalid parameter passed to ErrorViewModel.AddError");
            ErrorMessages.Add(error);
        }

    }
}
