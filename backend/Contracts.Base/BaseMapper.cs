using AutoMapper;

namespace Contracts.Base;

public class BaseMapper<TSource, TDestination>(IMapperBase mapper) : IMapper<TSource, TDestination>
{
    public virtual TSource Map(TDestination source)
    {
        return mapper.Map<TSource>(source);
    }

    public virtual TDestination Map(TSource source)
    {
        return mapper.Map<TDestination>(source);
    }
    
    public TDestination Map(TSource source, TDestination destination)
    {
        return mapper.Map(source, destination);
    }
    
    public TSource Map(TDestination source, TSource destination)
    {
        return mapper.Map(source, destination);
    }
    
    public TSource Map(TSource source, TSource destination)
    {
        return mapper.Map(source, destination);
    }
    
    public TDestination Map(TDestination source, TDestination destination)
    {
        return mapper.Map(source, destination);
    }
}