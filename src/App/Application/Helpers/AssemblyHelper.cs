using System.Reflection;

namespace Application.Helpers;

public static class AssemblyHelper
{
    public static Assembly GetApplicationLayerAssembly() => Assembly.GetAssembly(typeof(AssemblyHelper));
}