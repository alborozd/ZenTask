using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message)
            : base(message) { }
    }
}
