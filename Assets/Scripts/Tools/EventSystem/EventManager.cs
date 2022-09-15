using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;


public class CustomEvent : UnityEvent<object> { }

public static class EventManager
{
    private static Dictionary<eEventType, CustomEvent> eventsDictionary = new Dictionary<eEventType, CustomEvent>();

    public static void OnEvent(eEventType eventType, object obj)
    {
        if (eventsDictionary.TryGetValue(eventType, out CustomEvent unityEvent))
        {
            unityEvent.Invoke(obj);
        }
    }

    public static void OnEvent(eEventType eventType)
    {
        if (eventsDictionary.TryGetValue(eventType, out CustomEvent unityEvent))
        {
            unityEvent.Invoke(0);
        }
    }

    public static void Subscribe(eEventType eventType, UnityAction<object> action)
    {
        CustomEvent unityEvent = new CustomEvent();

        if (eventsDictionary.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.AddListener(action);
        }
        else
        {
            unityEvent = new CustomEvent();
            unityEvent.AddListener(action);
            eventsDictionary.Add(eventType, unityEvent);
        }
    }

    public static void Unsubscribe(eEventType eventType, UnityAction<object> action)
    {
        CustomEvent unityEvent = new CustomEvent();

        if (eventsDictionary.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.RemoveListener(action);
        }

    }
}
