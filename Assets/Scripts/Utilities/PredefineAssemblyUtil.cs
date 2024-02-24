using System;
using System.Collections.Generic;
using System.Reflection;

public static class PredefineAssemblyUtil
{
    public enum AssemblyType
    {
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpEditorFirstPass,
        AssemblyCSharpFirstPass
    }

    public static AssemblyType? GetAssemblyType(string _assemblyName)
    {
        return _assemblyName switch
        {
            "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
            "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
            _ => null
        };
    }

    public static List<Type> GetTypes(Type _interfaceType)
    {
        Assembly[] _assemlies = AppDomain.CurrentDomain.GetAssemblies();

        Dictionary<AssemblyType, Type[]> _assemblyTypes = new();
        List<Type> _types = new();
        AssemblyType? _assemblyType = null;

        for (int i = 0; i < _assemlies.Length; i++)
        {
            _assemblyType = GetAssemblyType(_assemlies[i].GetName().Name);

            if (_assemblyType != null)
            {
                _assemblyTypes.Add((AssemblyType)_assemblyType, _assemlies[i].GetTypes());
            }
        }

        AddTypesFromAssembly(_assemblyTypes[AssemblyType.AssemblyCSharp], _interfaceType, _types);
        AddTypesFromAssembly(_assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], _interfaceType, _types);

        return _types;
    }

    private static void AddTypesFromAssembly(Type[] _assembly, Type _interfaceType, ICollection<Type> _types)
    {
        if (_assembly == null) return;

        for (int i = 0; i < _assembly.Length; i++)
        {
            if (_assembly[i] != _interfaceType && _interfaceType.IsAssignableFrom(_assembly[i]))
            {
                _types.Add(_assembly[i]);
            }
        }
    }
}
