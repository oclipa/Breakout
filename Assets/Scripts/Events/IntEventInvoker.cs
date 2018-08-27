using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventInvoker : MonoBehaviour {

    protected Dictionary<EventName, UnityEvent<int>> intUnityEvents =
        new Dictionary<EventName, UnityEvent<int>>();

    /// <summary>
    /// Add the given listener for the given event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public void AddListener(EventName eventName, UnityAction<int> listener)
    {
        UnityEvent<int> unityEvent = null;
        if (intUnityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.AddListener(listener);
        }
    }

    /// <summary>
    /// Removes all listeners for the specified event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    public void RemoveAllListeners(EventName eventName)
    {
        UnityEvent<int> unityEvent = null;
        if (intUnityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveAllListeners();
        }
    }
}
