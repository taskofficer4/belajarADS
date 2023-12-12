using System;

namespace Actris.Infrastructure.HttpUtils
{
    public class OnErrorEventArgs : EventArgs
    {
        public OnErrorEventArgs(Exception e)
        {
            Exception = e;
        }
        public Exception Exception { get; }
    }
}
