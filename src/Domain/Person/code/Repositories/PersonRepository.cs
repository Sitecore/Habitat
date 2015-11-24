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
                Item = this.contextItem,
                Name = this.contextItem.GetString(Templates.Person.Fields.Name),
                Title = this.contextItem.GetString(Templates.Person.Fields.Title),
                Image = this.contextItem.GetImage(Templates.Person.Fields.Picture)
            };
        }
    }
}