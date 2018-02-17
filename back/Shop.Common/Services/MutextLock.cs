using Shop.Common.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Shop.Common.Services
{
    public class MutextLock : ILockService
    {
        private static Mutex _mutext = new Mutex();

        public void Execute(Action action)
        {
            try
            {
                _mutext.WaitOne();
                action();
            }
            finally
            {
                _mutext.ReleaseMutex();
            }
        }
    }
}
