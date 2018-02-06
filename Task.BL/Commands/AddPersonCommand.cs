using System;
using System.Data.Entity.Core;
using System.Linq;
using Task.Core.Domain;

namespace Task.BL.Commands
{
    public class AddPersonCommand : CommandBase<Person>
    {
        public string Lastname { get; set; }
        public string FirstName { get; set; }

        public AddPersonCommand(string firstname , string lastname)
        {
            CheckValuesParameter(firstname, lastname);
        }
        internal override void Execute(ITaskContext context)
        {
            if (!context.Database.Exists())
                throw new ObjectNotFoundException("Object not found");

            var createdAt = DateTime.UtcNow;
            var person = context.Persons.Create();
            person.FirstName = this.FirstName;
            person.LastName = this.FirstName;
            person.DateCreated = createdAt;
            context.Persons.Add(person);
            context.SaveChanges();
        }

        private void CheckValuesParameter(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Bad request");

            this.Lastname = lastname;
            this.FirstName = firstname;
        }
    }
}
