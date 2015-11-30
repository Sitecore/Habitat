namespace Habitat.Person.Repositories
{
    using System;
    using Framework.SitecoreExtensions.Extensions;
    using Models;
    using Sitecore.Data.Items;

    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly Item contextItem;

        public EmployeeRepository(Item contextItem)
        {
            if (contextItem == null)
            {
                throw new ArgumentException(nameof(contextItem));
            }

            if (!contextItem.IsDerived(Templates.Employee.ID))
            {
                throw new ArgumentException("It must derive from Employee", nameof(contextItem));
            }

            if (!contextItem.IsDerived(Templates.Person.ID))
            {
                throw new ArgumentException("It must derive from Person", nameof(contextItem));
            }

            this.contextItem = contextItem;
        }

        public Employee Get()
        {
            return new Employee
            {
                Person = new PersonRepository(this.contextItem).Get(),
                Description = this.contextItem.GetString(Templates.Employee.Fields.Description),
                Telephone = this.contextItem.GetString(Templates.Employee.Fields.Telephone),
                Mobile = this.contextItem.GetString(Templates.Employee.Fields.Mobile),
                Email = this.contextItem.GetString(Templates.Employee.Fields.Email),
                FacebookLink = this.contextItem.GetString(Templates.Employee.Fields.FacebookLink),
                TwitterLink = this.contextItem.GetString(Templates.Employee.Fields.TwitterLink),
                LinkedInLink = this.contextItem.GetString(Templates.Employee.Fields.LinkedInLink),
                BlogLink = this.contextItem.GetString(Templates.Employee.Fields.BlogLink)
            };
        }
    }
}