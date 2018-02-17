using System.Globalization;
using System.Xml;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;

namespace Shop.XmlDal.Repositories
{
    public class TransactionsRepository : BaseRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Transactions.xml";

        protected override Transaction GetEntity(XmlNode node)
        {
            string username = node.SelectSingleNode("UserName")?.InnerText;
            string productIdStr = node.SelectSingleNode("ProductId")?.InnerText;
            string productName = node.SelectSingleNode("ProductName")?.InnerText;
            string qtyStr = node.SelectSingleNode("Quantity")?.InnerText;
            string costStr = node.SelectSingleNode("Cost")?.InnerText;
            string discountStr = node.SelectSingleNode("Discount")?.InnerText;
            string sellCostStr = node.SelectSingleNode("SellCost")?.InnerText;
            string amountStr = node.SelectSingleNode("Amount")?.InnerText;

            int.TryParse(productIdStr, out int productId);
            int.TryParse(qtyStr, out int qty);
            decimal.TryParse(costStr, out decimal cost);
            float.TryParse(discountStr, out float discount);
            decimal.TryParse(sellCostStr, out decimal sellCost);
            decimal.TryParse(amountStr, out decimal amount);

            return new Transaction()
            {
                Username = username,
                ProductId = productId,
                ProductName = productName,
                Amount = amount,
                Cost = cost,
                Discount = discount,
                Quantity = qty,
                SellCost = sellCost
            };
        }

        protected override XmlElement SetEntity(Transaction entity, XmlDocument doc)
        {
            XmlElement transaction = doc.CreateElement("Transaction");
            XmlElement username = doc.CreateElement("UserName");
            username.AppendChild(doc.CreateTextNode(entity.Username));

            XmlElement productId = doc.CreateElement("ProductId");
            productId.AppendChild(doc.CreateTextNode(entity.ProductId.ToString()));

            XmlElement productName = doc.CreateElement("ProductName");
            productName.AppendChild(doc.CreateTextNode(entity.ProductName));

            XmlElement quantity = doc.CreateElement("Quantity");
            quantity.AppendChild(doc.CreateTextNode(entity.Quantity.ToString()));

            XmlElement cost = doc.CreateElement("Cost");
            cost.AppendChild(doc.CreateTextNode(entity.Cost.ToString(CultureInfo.InvariantCulture)));

            XmlElement discount = doc.CreateElement("Discount");
            discount.AppendChild(doc.CreateTextNode(entity.Discount.ToString(CultureInfo.InvariantCulture)));

            XmlElement sellCost = doc.CreateElement("SellCost");
            sellCost.AppendChild(doc.CreateTextNode(entity.SellCost.ToString(CultureInfo.InvariantCulture)));

            XmlElement amount = doc.CreateElement("Amount");
            amount.AppendChild(doc.CreateTextNode(entity.Amount.ToString(CultureInfo.InvariantCulture)));

            transaction.AppendChild(username);
            transaction.AppendChild(productId);
            transaction.AppendChild(productName);
            transaction.AppendChild(quantity);
            transaction.AppendChild(cost);
            transaction.AppendChild(discount);
            transaction.AppendChild(sellCost);
            transaction.AppendChild(amount);

            return transaction;
        }
    }
}
