using Application.Helpers;
using AutoMapper;
using Xunit;

namespace MappingTests;
public class MapperAssertion
{

    [Fact]
    public void Verify_AutoMapper_Config()
    {
        IConfigurationProvider config = new MapperConfiguration(x => x.AddMaps(AssemblyHelper.GetApplicationLayerAssembly()));
        config.AssertConfigurationIsValid();
    }
}