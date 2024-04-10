using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App
{
    public class Organization : DomainEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(256)]
        public string RegistrationNumber { get; set; } = default!;

        [Required]
        public int NumberOfParticipants { get; set; }

        [MaxLength(5000)]
        public string? Description { get; set; }

        [Required, ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }

        [Required, ForeignKey(nameof(PaymentMethod))]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
