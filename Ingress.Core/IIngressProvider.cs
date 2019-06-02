using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ingress.Core
{
    public interface IIngressProvider
    {
        (int, Exception) ProcessData(IDictionary<string, object> settings);
        Task<(int, Exception)> ProcessDataAsync(IDictionary<string, object> settings);
    }
}
