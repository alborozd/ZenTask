using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Shop.XmlDal.Repositories
{
    public class TransacionErrorsRepository : BaseRepository<TransactionError>, ITransactionErrorsRepository
    {
        public TransacionErrorsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "TransactionErrors.xml";

        protected override TransactionError GetEntity(XmlNode node)
        {
            string dateStr = node.SelectSingleNode("Date")?.InnerText;
            string description = node.SelectSingleNode("Description")?.InnerText;
            DateTime date = DateTime.Parse(dateStr, CultureInfo.InvariantCulture);

            return new TransactionError()
            {
                Date = date, 
                Description = description
            };
        }

        protected override XmlElement SetEntity(TransactionError entity, XmlDocument doc)
        {
            XmlElement error = doc.CreateElement("Error");
            XmlElement date = doc.CreateElement("Date");
            date.AppendChild(doc.CreateTextNode(entity.Date.ToString(CultureInfo.InvariantCulture)));

            XmlElement description = doc.CreateElement("Description");
            description.AppendChild(doc.CreateTextNode(entity.Description));

            error.AppendChild(date);
            error.AppendChild(description);

            return error;
        }
    }
}
