using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Entities;
using Mapster;

namespace EurobusinessHelper.Application.Mappings;

public class MappingRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        //register mappings
        CreateMap<Identity, IdentityDto>(config)
            //custom mapping logic here
            // .Map(d => d.Name, s => string.IsNullOrWhiteSpace(s.Name) ? "John Doe" : s.Name)
            ;
    }

    private static TypeAdapterSetter<TSource, TDestination> CreateMap<TSource, TDestination>(
        CodeGenerationConfig config)
    {   
        //code generation
        config.AdaptTo(typeof(TDestination).Name)
            .ForType<TSource>();
        config.GenerateMapper("[name]Mapper")
            .ForType<TSource>();
        
        //new config for type
        return TypeAdapterConfig<TSource, TDestination>
            .NewConfig();
    }
}