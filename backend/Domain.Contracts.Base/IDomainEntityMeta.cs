namespace Domain.Contracts.Base;

public interface IDomainEntityMeta
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}