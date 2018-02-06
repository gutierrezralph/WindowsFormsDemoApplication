using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Core.Domain
{
    public class Employee : IEntity
    {
        public Employee()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
