using Contracts.Base;

namespace Extensions.Base.Mappers;

public class BaseMapper<TSource, TDestination> : IMapper<TSource, TDestination>
{
    private readonly AutoMapper.IMapper _mapper;

    protected BaseMapper(AutoMapper.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TSource Map(TDestination source)
    {
        return _mapper.Map<TSource>(source);
    }

    public TDestination Map(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }

    public TSource Map(TDestination source, TSource destination)
    {
        return _mapper.Map(source, destination);
    }
    
    public TSource Map(TSource source, TSource destination)
    {
        return _mapper.Map(source, destination);
    }
    
    public TDestination Map(TDestination source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }
}