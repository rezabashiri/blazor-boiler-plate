using System.Reflection;

namespace Mapping.Helpers;

public static class AssemblyHelper
{
    public static Assembly GetApplicationLayerAssembly() => Assembly.GetAssembly(typeof(AssemblyHelper));
}