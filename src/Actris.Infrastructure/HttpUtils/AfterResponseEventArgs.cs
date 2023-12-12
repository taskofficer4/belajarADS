using System;
using System.Net.Http;

namespace Actris.Infrastructure.HttpUtils
{
    public class AfterResponseEventArgs : EventArgs
    {
        public HttpResponseMessage Response { get; internal set; }
    }
}
