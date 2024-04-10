using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class PaymentMethod : DomainEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = default!;

    }
}
