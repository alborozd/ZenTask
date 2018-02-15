using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using Shop.XmlDal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Shop.XmlDal
{
    public class DiscountsRepository : ReadRepository<Discount>, IDiscountsRepository
    {
        public DiscountsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => "Discounts.xml";

        private Tuple<int, DateTime, float> ParseResult(string productIdStr, string dateStr, string percentsStr)
        {
            int productId = 0;
            int.TryParse(productIdStr, out productId);

            DateTime date = DateTime.Now; //TODO: parse date correctly
            DateTime.TryParse(dateStr, out date);

            float percents = 0;
            float.TryParse(percentsStr, out percents);

            return new Tuple<int, DateTime, float>(productId, date, percents);

        }

        protected override Discount GetEntity(XmlNode node)
        {
            string productIdStr = node.SelectSingleNode("@ProductId")?.Value;
            string dateStr = node.SelectSingleNode("StartDate")?.InnerText;
            string percentsStr = node.SelectSingleNode("Percents")?.InnerText;

            var result = ParseResult(productIdStr, dateStr, percentsStr);

            return new Discount()
            {
                ProductId = result.Item1,
                StartDate = result.Item2,
                Percents = result.Item3
            };
        }

        public IEnumerable<Discount> GetDiscountsByProduct(int productId)
        {
            return XmlRoot
                .SelectNodes($"Discount[@ProductId='{productId}']")
                ?.Cast<XmlNode>()
                ?.Select(node => GetEntity(node)) ?? Enumerable.Empty<Discount>();
        }
    }
}
