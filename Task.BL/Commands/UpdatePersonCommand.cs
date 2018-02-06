using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL.Commands
{
    public class UpdatePersonCommand : CommandBase
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string FirstName { get; set; }

        public UpdatePersonCommand(int Id, string firstname, string lastname)
        {
            CheckValuesParameter(Id,firstname, lastname);
        }
        internal override void Execute(ITaskContext context)
        {
            if (!context.Database.Exists())
                throw new ObjectNotFoundException("Object not found");

            var q = from p in context.Persons
                    where p.Id == this.Id
                    select p;

            var entity = q.FirstOrDefault();

            if (!entity.Equals(null))
            {
                var createdAt = DateTime.UtcNow;
                entity.FirstName = this.FirstName;
                entity.LastName = this.FirstName;
                entity.DateCreated = createdAt;
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void CheckValuesParameter(int id, string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) || string.IsNullOrWhiteSpace(id.ToString()) || id == 0)
                throw new ArgumentException("Bad request");

            this.Id = id;
            this.Lastname = lastname;
            this.FirstName = firstname;
        }

    }
}
