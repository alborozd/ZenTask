using System;
using System.Xml;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;

namespace Shop.XmlDal.Repositories
{
    public class ProductsRepository : ReadRepository<Product>, IProductsRepository
    {
        public ProductsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Products.xml";     

        protected override Product GetEntity(XmlNode node)
        {
            string idStr = node.SelectSingleNode("Id")?.InnerText;
            string name = node.SelectSingleNode("Name")?.InnerText;
            string costStr = node.SelectSingleNode("Cost")?.InnerText;
            string qtyStr = node.SelectSingleNode("Quantity")?.InnerText;

            decimal.TryParse(costStr, out decimal cost);
            int.TryParse(qtyStr, out int qty);
            int.TryParse(idStr, out int id);
            
            return new Product()
            {
                Id = id,
                Name = name,
                Cost = cost,
                Quantity = qty
            };
        }

        public void ChangeQuantity(int productId, int newQuantity)
        {
            string path = pathResolver.GetXmlPath(FileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            
            XmlNode node = root.SelectSingleNode($"Product[Id={productId}]");
            if (node == null)
                return;

            XmlNode quantity = node.SelectSingleNode("Quantity");
            if (quantity == null)
                return;

            quantity.InnerText = newQuantity.ToString();
            doc.Save(path);
        }
    }
}
