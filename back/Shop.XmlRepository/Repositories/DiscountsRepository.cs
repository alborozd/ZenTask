using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;

namespace Shop.XmlDal.Repositories
{
    public class DiscountsRepository : ReadRepository<Discount>, IDiscountsRepository
    {
        public DiscountsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Discounts.xml";

        protected override Discount GetEntity(XmlNode node)
        {
            string productIdStr = node.SelectSingleNode("@ProductId")?.Value;
            string dateStr = node.SelectSingleNode("StartDate")?.InnerText;
            string percentsStr = node.SelectSingleNode("Percents")?.InnerText;

            DateTime date = DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
            int.TryParse(productIdStr, out int productId);
            float.TryParse(percentsStr, out float percents);

            return new Discount()
            {
                ProductId = productId,
                StartDate = date,
                Percents = percents
            };
        }

        public IEnumerable<Discount> GetDiscountsByProduct(int productId)
        {
            return XmlRoot
                .SelectNodes($"Discount[@ProductId='{productId}']")
                ?.Cast<XmlNode>()
                ?.Select(GetEntity) ?? Enumerable.Empty<Discount>();
        }
    }
}
