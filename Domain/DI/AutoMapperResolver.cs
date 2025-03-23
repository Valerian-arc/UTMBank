using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class AutoMapperResolver : IDependencyResolver
{
    private readonly IMapper _mapper;

    public AutoMapperResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public object GetService(Type serviceType)
    {
        return serviceType == typeof(IMapper) ? _mapper : null;
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return new List<object>();
    }
}
