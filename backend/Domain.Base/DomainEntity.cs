using Domain.Contracts.Base;

namespace Domain.Base;

public abstract class DomainEntity : DomainEntity<Guid>;

public abstract class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
    where TKey : IEquatable<TKey>;