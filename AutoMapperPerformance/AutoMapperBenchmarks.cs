namespace AutoMapperPerformance;

using AutoMapper;
using BenchmarkDotNet.Attributes;

public class AutoMapperBenchmarks
{
    private readonly IMapper _defaultMapper = AutoMapperFactory.CreateDefaultMapper();
    private readonly IMapper _explicitMapper = AutoMapperFactory.CreateExplicitMapper();
    private readonly IMapper _explicitStaticMapper = AutoMapperFactory.CreateExplicitStaticMapper();

    private readonly IReadOnlyCollection<Foo> _sourceObjects;

    public AutoMapperBenchmarks()
    {
        _sourceObjects = CreateSourceObjects();

        // Warm-up mappers
        var sourceObject = _sourceObjects.First();
        _defaultMapper.Map<Bar>(sourceObject);
        _explicitMapper.Map<Bar>(sourceObject);
        _explicitStaticMapper.Map<Bar>(sourceObject);
    }

    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<Bar> DefaultMapper()
    {
        return _defaultMapper.Map<List<Bar>>(_sourceObjects);
    }

    [Benchmark]
    public IReadOnlyCollection<Bar> ExplicitMapper()
    {
        return _explicitMapper.Map<List<Bar>>(_sourceObjects);
    }

    [Benchmark]
    public IReadOnlyCollection<Bar> ExplicitStaticMapper()
    {
        return _explicitStaticMapper.Map<List<Bar>>(_sourceObjects);
    }

    private static IReadOnlyCollection<Foo> CreateSourceObjects()
    {
        var sourceObjects = new List<Foo>();

        for (var i = 0; i < 100; i++)
        {
            sourceObjects.Add(
                new Foo
                {
                    Id = 1 * 1_000_000,
                    LastName = $"LastName_{i}",
                    FirstName = $"{i}_FirstName",
                    Salary = i * 100m,
                    Weight = i * 2,
                    UniqueId = Guid.NewGuid()
                });
        }

        return sourceObjects;
    }
}