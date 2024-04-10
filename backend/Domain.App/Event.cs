using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Event : DomainEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = default!;

        [Required]
        public DateTime DateTime { get; set; }

        [Required, MaxLength(256)]
        public string Location { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public ICollection<Person>? People { get; set; }
        public ICollection<Organization>? Organizations { get; set; }
    }
}
