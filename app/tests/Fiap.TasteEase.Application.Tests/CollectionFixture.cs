using System.Reflection;
using Mapster;

namespace Fiap.TasteEase.Application.Tests;

public class CollectionFixture
{
    public CollectionFixture()
    {
        ConfigMapster();
    }
    
    private void ConfigMapster()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        var mappersAssemblies = Array.Empty<Assembly>();

        mappersAssemblies = mappersAssemblies.Append(typeof(DependencyInjection).Assembly).ToArray();

        config.Scan(assemblies: mappersAssemblies);
        config.Default.AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);
        config.Compile();
    }
}

[CollectionDefinition("Collection_Fixture")]
public class DatabaseCollection : ICollectionFixture<CollectionFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}