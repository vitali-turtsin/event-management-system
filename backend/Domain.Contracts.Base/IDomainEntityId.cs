namespace Domain.Contracts.Base;

public interface IDomainEntityId : IDomainEntityId<Guid>;

public interface IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}