using System.Data.Entity.Core;
using System.Linq;


namespace Task.BL.Commands
{
    public class DeletePersonCommand : CommandBase
    {
        public int Id { get; set; }
        public DeletePersonCommand(int id)
        {
            this.Id = id;
        }
        internal override void Execute(ITaskContext context)
        {
            if (!context.Database.Exists())
                throw new ObjectNotFoundException("Object not found");

            if (this.Id == 0)
                return;

            var result = context.Persons.Where(p => p.Id == this.Id);

            foreach (var person in result)
            {
                context.Persons.Remove(person);
            }
            context.SaveChanges();
        }
    }
}
