using Shop.ConsoleClient.Core.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Exceptions
{
    public class ScreenException : Exception
    {        
        public ScreenException(string message)
            : base(message) { }

        public ScreenException(string message, IScreen screen)
            : base(message)
        {
            ScreenToShow = screen;
        }

        public IScreen ScreenToShow { get; set; }
    }
}
