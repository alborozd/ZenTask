using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop.XmlDal.Repositories
{
    public abstract class ReadRepository<T> : IReadRepository<T>
        where T : Entity
    {
        protected readonly IXmlPathResolver pathResolver;

        public ReadRepository(IXmlPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        protected abstract string FileName { get; }

        protected XmlElement XmlRoot
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathResolver.GetXmlPath(FileName));
                XmlElement root = doc.DocumentElement;

                return root;
            }
        }

        protected abstract T GetEntity(XmlNode node);

        public IEnumerable<T> GetAll()
        {
            List<T> items = new List<T>();

            foreach (XmlElement node in XmlRoot)
            {
                items.Add(GetEntity(node));
            }

            return items;
        }
    }
}
