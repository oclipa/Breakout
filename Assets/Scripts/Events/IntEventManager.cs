using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class IntEventManager
{

    #region Fields

    static Dictionary<EventName, List<IntEventInvoker>> invokers =
        new Dictionary<EventName, List<IntEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<int>>> listeners =
        new Dictionary<EventName, List<UnityAction<int>>>();

    #endregion

    #region Public methods

    /// <summary>
    /// Initializes the event manager
    /// </summary>
    public static void Initialize()
    {
        foreach(EventName name in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(name))
            {
                invokers.Add(name, new List<IntEventInvoker>());
                listeners.Add(name, new List<UnityAction<int>>());
            }
            else
            {
                invokers[name].Clear();
                listeners[name].Clear();
            }
        }
    }

    /// <summary>
    /// Adds the given invoker for the given event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="invoker">Invoker.</param>
    public static void AddInvoker(EventName eventName, IntEventInvoker invoker)
    {
        foreach(UnityAction<int> listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        invokers[eventName].Add(invoker);
    }

    /// <summary>
    /// Adds the given listener for the given event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void AddListener(EventName eventName, UnityAction<int> listener)
    {
        foreach(IntEventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        listeners[eventName].Add(listener);
    }

    /// <summary>
    /// Removes the given invoker for the given eventy name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="invoker">Invoker.</param>
    public static void RemoveInvoker(EventName eventName, IntEventInvoker invoker)
    {
        // remove invoker from dictionary
        invokers[eventName].Remove(invoker);
    }

    #endregion
}
