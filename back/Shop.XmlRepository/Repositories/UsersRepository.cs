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
