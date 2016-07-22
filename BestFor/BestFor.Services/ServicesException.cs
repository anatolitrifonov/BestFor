using System;

namespace BestFor.Services
{
    public class ServicesException : Exception
    {
        public ServicesException(string message) : base(message)
        {
        }
    }
}
