namespace Contracts.Base;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
    TSource Map(TDestination source);
    TDestination Map(TSource source, TDestination destination);
    TSource Map(TDestination source, TSource destination);
    TSource Map(TSource source, TSource destination);
    TDestination Map(TDestination source, TDestination destination);
}
