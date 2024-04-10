using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Base;

namespace Domain.Base;

public class DomainEntityMeta : IDomainEntityMeta
{
    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}