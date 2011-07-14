using System;

namespace SirenOfShame.Lib.Exceptions
{
    public class SosException : Exception
    {
        public SosException()
        {
        }
        
        public SosException(string message) : base(message)
        {
        }

        public SosException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
