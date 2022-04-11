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
            .Map(d => d.Name, s => s.FirstName + " " + s.LastName);
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