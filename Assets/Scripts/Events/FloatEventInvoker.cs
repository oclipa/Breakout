using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventInvoker : MonoBehaviour
{

    protected Dictionary<EventName, UnityEvent<float>> floatUnityEvents =
        new Dictionary<EventName, UnityEvent<float>>();

    /// <summary>
    /// Add the given listener for the given event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public void AddListener(EventName eventName, UnityAction<float> listener)
    {
        UnityEvent<float> unityEvent = null;
        if (floatUnityEvents.TryGetValue(eventName, out unityEvent))
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
        UnityEvent<float> unityEvent = null;
        if (floatUnityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveAllListeners();
        }
    }
}
