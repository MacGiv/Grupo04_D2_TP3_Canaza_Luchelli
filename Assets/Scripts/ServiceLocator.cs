using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    public static void AddService<T>(T service) where T : IService
    {
        var type = typeof(T);
        if (!_services.TryAdd(type, service))
            Debug.LogWarning("Ya está suscripto: " + type);
    }

    public static void RemoveService<T>(T service) where T : IService
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
            _services.Remove(type);
        else
            Debug.LogWarning("No se encontró el servicio: " + type);
    }

    public static T GetService<T>() where T : class
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
            return (T)service;

        Debug.LogError("No se encontró el servicio: " + type);
        return null;
    }

    public static void ClearServices() => _services.Clear();

    public static void InitializeServices(GameSettingsSo gameSettings)
    {
        foreach (var service in _services)
            service.Value.Initialize(gameSettings);
    }

    public static void DeInitializeServices()
    {
        foreach (var service in _services)
            service.Value.DeInitialize();
    }
}