using Domain.Base;
using Extensions.Base.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class Person : DomainEntity
    {
        [Required, MaxLength(256)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(256)]
        public string LastName { get; set; } = default!;

        [Required, PersonalCode]
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
