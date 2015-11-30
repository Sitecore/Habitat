namespace Habitat.Person.Repositories
{
    using System;
    using Habitat.Framework.SitecoreExtensions.Extensions;
    using Models;
    using Sitecore.Data.Items;

    internal class PersonRepository :IPersonRepository
    {
        private readonly Item contextItem;

        public PersonRepository(Item contextItem)
        {
            if (contextItem == null)
            {
                throw new ArgumentException(nameof(contextItem));
            }

            if (!contextItem.IsDerived(Templates.Person.ID))
            {
                throw new ArgumentException("It must derive from Person", nameof(contextItem));
            }

            this.contextItem = contextItem;
        }

        public Person Get()
        {
            return new Person
            {
                Name = this.contextItem.Field(Templates.Person.Fields.Name),
                Title = this.contextItem.Field(Templates.Person.Fields.Title),
                Picture = this.contextItem.Field(Templates.Person.Fields.Picture)
            };
        }
    }
}