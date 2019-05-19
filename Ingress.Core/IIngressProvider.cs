using System;
using System.Collections.Generic;
using System.Text;

namespace Ingress.Core
{
    public interface IIngressProvider
    {
        (int, Exception) ProcessData(IDictionary<string, object> settings);
    }
}
