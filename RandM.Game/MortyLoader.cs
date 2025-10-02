using System.Reflection;
using RandM.Abstractions;

public static class MortyLoader
{
    public static IMorty Load(string assemblyPath, string classFullName)
    {
        if (!File.Exists(assemblyPath))
            throw new UserFriendlyException($"Morty assembly not found: {assemblyPath}",
                "Make sure the path points to the built DLL of the Morty project.");

        var asm = Assembly.LoadFrom(assemblyPath);
        var type = asm.GetType(classFullName, throwOnError: false);
        if (type is null)
            throw new UserFriendlyException($"Morty class \"{classFullName}\" not found in {assemblyPath}.",
                "Check the namespace and class name. E.g., RandM.Morties.Classic.ClassicMorty");

        if (!typeof(IMorty).IsAssignableFrom(type))
            throw new UserFriendlyException($"Class {classFullName} does not implement IMorty.",
                "Ensure the Morty project references RandM.Abstractions and implements IMorty.");

        var inst = Activator.CreateInstance(type) as IMorty;
        if (inst is null)
            throw new UserFriendlyException($"Could not instantiate {classFullName}.");

        return inst;
    }
}
