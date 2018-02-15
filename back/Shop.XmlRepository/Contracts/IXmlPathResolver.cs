using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.XmlDal.Contracts
{
    public interface IXmlPathResolver
    {
        string GetXmlPath(string fileName);
    }
}
