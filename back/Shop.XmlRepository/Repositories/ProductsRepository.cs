using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using Shop.XmlDal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop.XmlDal
{
    public class ProductsRepository : ReadRepository<Product>, IProductsRepository
    {
        public ProductsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Products.xml";

        private Tuple<decimal, int, int> ParseResult(string costStr, string qtyStr, string idStr)
        {
            decimal cost = 0;
            decimal.TryParse(costStr, out cost);

            int qty = 0;
            int.TryParse(qtyStr, out qty);

            int id = 0;
            int.TryParse(idStr, out id);

            return new Tuple<decimal, int, int>(cost, qty, id);
        }        

        protected override Product GetEntity(XmlNode node)
        {
            string idStr = node.SelectSingleNode("Id")?.InnerText;
            string name = node.SelectSingleNode("Name")?.InnerText;
            string costStr = node.SelectSingleNode("Cost")?.InnerText;
            string qtyStr = node.SelectSingleNode("Quantity")?.InnerText;

            var parsedResult = ParseResult(costStr, qtyStr, idStr);

            return new Product()
            {
                Id = parsedResult.Item3,
                Name = name,
                Cost = parsedResult.Item1,
                Quantity = parsedResult.Item2
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
