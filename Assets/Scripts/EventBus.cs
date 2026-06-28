using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> _subscribers = new();

    public static void Subscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);

        if (_subscribers.TryGetValue(eventType, out Delegate existingDelegate))
            _subscribers[eventType] = Delegate.Combine(existingDelegate, listener);
        else
            _subscribers[eventType] = listener;
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);

        if (!_subscribers.TryGetValue(eventType, out Delegate existingDelegate))
            return;

        Delegate newDelegate = Delegate.Remove(existingDelegate, listener);

        if (newDelegate == null)
            _subscribers.Remove(eventType);
        else
            _subscribers[eventType] = newDelegate;
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (_subscribers.TryGetValue(eventType, out Delegate existingDelegate))
            ((Action<T>)existingDelegate)?.Invoke(eventData);
    }

    public static void Clear() => _subscribers.Clear();
}