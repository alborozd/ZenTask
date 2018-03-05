using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Screens
{
    public interface IScreen
    {
        void Show();
        IScreen HandleInput(string input);
    }
}
