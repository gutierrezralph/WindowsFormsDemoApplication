namespace Task.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task.Implementation.TaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task.Implementation.TaskContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Employees.AddOrUpdate(e => e.Id,
                new Core.Domain.Employee { Id = 1, FirstName = "Ralph", LastName = "Gutierrez", DateCreated = DateTime.Now },
                new Core.Domain.Employee { Id = 2, FirstName = "Joshua", LastName = "Colanggo", DateCreated = DateTime.Now }
                );
        }
    }
}
