using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
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

        [Required]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}
