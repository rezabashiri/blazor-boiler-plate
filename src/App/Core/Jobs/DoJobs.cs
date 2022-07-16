using System.Reflection;
using Core.Comparers;
using Core.Jobs.Interfaces;

namespace Core.Jobs;

/// <summary>
/// Make an instance of a job and feed its constructors parameters
/// All you need to do is just inherit from <see cref="IJob"/>
/// </summary>
public static class DoJobs
{

    private static readonly IEqualityComparer<Type> TypeInheritanceComparer =
        AdHocEqualityComparer<Type>
            .CreateWithoutHashCode((candidate, parameter) => parameter.IsAssignableFrom(candidate));
    public static void DoAll(params object[] candidates)
    {
        var jobExpectedAssemblies = new List<string> { "Application", "Web", "Core" };
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => jobExpectedAssemblies.Any(name => assembly.FullName.StartsWith(name)));
        foreach (var assembly in assemblies) {
            var jobs = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IJob)));

            foreach (var job in jobs) {
                var constructor = job.GetConstructors().Single();

                var parameters =
                    constructor
                        .GetParameters()
                        .Join(
                            candidates,
                            parameter => parameter.ParameterType,
                            candidate => candidate.GetType(),
                            (parameter, candidate) => candidate,
                            TypeInheritanceComparer
                        ).ToArray();

                var dependenciesResolved = (parameters.Length == constructor.GetParameters().Length);
                if (dependenciesResolved) {
                    var instance = (IJob)Activator.CreateInstance(job, parameters);
                    instance.Todo();
                }
            }
        }
    }
}