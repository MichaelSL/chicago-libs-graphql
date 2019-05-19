using Ingress.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ingress.Core
{
    public interface IDataProvider
    {
        void SaveData(IEnumerable<Library> libraries);
    }
}
