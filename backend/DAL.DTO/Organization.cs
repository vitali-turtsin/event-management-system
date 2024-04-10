using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class Organization : DomainEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(256)]
        public string? RegistrationNumber { get; set; }

        [Required]
        public int NumberOfParticipants { get; set; }

        [MaxLength(5000)]
        public string? Description { get; set; }

        [Required]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}
