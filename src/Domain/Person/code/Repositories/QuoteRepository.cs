namespace Habitat.Person.Repositories
{
    using System;
    using Framework.SitecoreExtensions.Extensions;
    using Models;
    using Sitecore.Data.Items;

    public class QuoteRepository : IQuoteRepository
    {
        private readonly Item contextItem;

        public object Parameters { get; set; }

        public QuoteRepository(Item contextItem)
        {
            if (contextItem == null)
            {
                throw new ArgumentException(nameof(contextItem));
            }

            if (!contextItem.IsDerived(Templates.Person.ID))
            {
                throw new ArgumentException("Must be derived from Person", nameof(contextItem));
            }

            if (!contextItem.IsDerived(Templates.Quote.ID))
            {
                throw new ArgumentException("Must be derived from Quote", nameof(contextItem));
            }

            this.contextItem = contextItem;
        }

        public Quote Get()
        {
            return new Quote
            {
                Person = new PersonRepository(this.contextItem).Get(),
                Company = this.contextItem.Field(Templates.Quote.Fields.Company),
                Quotation = this.contextItem.Field(Templates.Quote.Fields.Quote)
            };
        }
    }
}