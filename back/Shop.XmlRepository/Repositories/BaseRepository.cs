using System.Xml;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;

namespace Shop.XmlDal.Repositories
{
    public abstract class BaseRepository<T> : ReadRepository<T>, IBaseRepository<T> 
        where T : Entity
    {        
        public BaseRepository(IXmlPathResolver pathResolver)
            : base(pathResolver) { }
        
        protected abstract XmlElement SetEntity(T entity, XmlDocument doc);

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
    }
}
