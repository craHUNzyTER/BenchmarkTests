namespace AutoMapperPerformance;

using AutoMapper;

public static class AutoMapperFactory
{
    public static IMapper CreateDefaultMapper()
    {
        var config = new MapperConfiguration(cfg => { cfg.CreateMap<Foo, Bar>(); });

        return config.CreateMapper();
    }

    public static IMapper CreateExplicitMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Foo, Bar>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.UniqueId));
        });

        return config.CreateMapper();
    }

    public static IMapper CreateExplicitStaticMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Foo, Bar>()
                .ForMember(static dest => dest.Id, static opt => opt.MapFrom(static src => src.Id))
                .ForMember(static dest => dest.LastName, static opt => opt.MapFrom(static src => src.LastName))
                .ForMember(static dest => dest.FirstName, static opt => opt.MapFrom(static src => src.FirstName))
                .ForMember(static dest => dest.Salary, static opt => opt.MapFrom(static src => src.Salary))
                .ForMember(static dest => dest.Weight, static opt => opt.MapFrom(static src => src.Weight))
                .ForMember(static dest => dest.UniqueId, static opt => opt.MapFrom(static src => src.UniqueId));
        });

        return config.CreateMapper();
    }
}