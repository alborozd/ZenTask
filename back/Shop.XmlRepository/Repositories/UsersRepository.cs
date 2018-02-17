using System.Globalization;
using System.Xml;
using Shop.Common;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;

namespace Shop.XmlDal.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Users.xml";

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

        public void ChangeBalance(string username, decimal newBalance)
        {
            string path = pathResolver.GetXmlPath(FileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            doc.DocumentElement
                .SelectSingleNode($"User[Name='{username}']")
                .Return(node => node.SelectSingleNode("Amount"))
                .Do(balance => balance.InnerText = newBalance.ToString(CultureInfo.InvariantCulture));

            doc.Save(path);
        }

        public User GetByName(string name)
        {
            return XmlRoot
                .SelectSingleNode($"User[Name='{name}']")
                .Return(GetEntity, null);
        }

        protected override User GetEntity(XmlNode node)
        {
            string username = node.SelectSingleNode("Name")?.InnerText;
            string amountStr = node.SelectSingleNode("Amount")?.InnerText;

            decimal.TryParse(amountStr, out decimal amount);

            return new User()
            {
                Name = username,
                Amount = amount
            };
        }

        protected override XmlElement SetEntity(User entity, XmlDocument doc)
        {
            XmlElement userRoot = doc.CreateElement("User");
            XmlElement username = doc.CreateElement("Name");
            username.AppendChild(doc.CreateTextNode(entity.Name));

            XmlElement amount = doc.CreateElement("Amount");
            amount.AppendChild(doc.CreateTextNode(entity.Amount.ToString(CultureInfo.InvariantCulture)));

            userRoot.AppendChild(username);
            userRoot.AppendChild(amount);

            return userRoot;
        }
    }
}
