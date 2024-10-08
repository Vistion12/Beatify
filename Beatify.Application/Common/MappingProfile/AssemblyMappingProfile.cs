﻿using AutoMapper;
using System.Reflection;

namespace Beatify.Application.Common.MappingProfile;

public class AssemblyMappingProfile:Profile
{
    public AssemblyMappingProfile(Assembly assembly)=>
        ApplyMappingsFromAssembly(assembly);
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types= assembly.GetExportedTypes()
            .Where(t=>t.GetInterfaces()
            .Any(i=>i.IsGenericType && 
            i.GetGenericTypeDefinition()==typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types) 
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance,[this]);
        }
    }
}
