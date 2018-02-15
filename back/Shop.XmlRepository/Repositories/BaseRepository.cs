using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace Shop.XmlDal
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected IXmlPathResolver pathResolver;

        public BaseRepository(IXmlPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        protected abstract string FileName { get; }
        protected abstract XmlElement SetEntity(T entity, XmlDocument doc);
        protected abstract T GetEntity(XmlElement node);

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

        public T Add(T entity)
        {
            string filePath = pathResolver.GetXmlPath(FileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement root = doc.DocumentElement;
            root.AppendChild(SetEntity(entity, doc));
            doc.Save(filePath);

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            List<T> items = new List<T>();
            
            foreach(XmlElement node in XmlRoot)
            {
                items.Add(GetEntity(node));
            }

            return items;
        }
    }
}
