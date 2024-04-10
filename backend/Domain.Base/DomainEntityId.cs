using Domain.Contracts.Base;

namespace Domain.Base;

public abstract class DomainEntityId : DomainEntityId<Guid>;

public abstract class DomainEntityId<TKey> : DomainEntityMeta, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}