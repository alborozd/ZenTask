using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shop.XmlDal
{
    public class XmlPathResolver : IXmlPathResolver
    {
        public string GetXmlPath(string fileName)
        {
            return Path.Combine("Xml", fileName);
        }
    }
}
