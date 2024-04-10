using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App
{
    public class Person : DomainEntity
    {
        [Required, MaxLength(256)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(256)]
        public string LastName { get; set; } = default!;

        [Required]
        public string PersonalCode { get; set; } = default!;

        [MaxLength(1500)]
        public string? Description { get; set; }

        [Required, ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }

        [Required, ForeignKey(nameof(PaymentMethod))]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
