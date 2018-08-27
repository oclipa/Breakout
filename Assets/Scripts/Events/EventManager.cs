using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event manager.
/// </summary>
public static class EventManager
{
    static List<PickupBlock> freezerEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> freezerEffectListeners = new List<UnityAction<float>>();

    static List<PickupBlock> speedupEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> speedupEffectListeners = new List<UnityAction<float>>();

    #region FreezerEffect

    public static void AddFreezerEffectInvoker(PickupBlock freezerEffectInvoker)
    {
        // add the new invoker to the list of invokers
        freezerEffectInvokers.Add(freezerEffectInvoker);

        // ensure that all existing listeners are added to this new invoker
        foreach (UnityAction<float> listener in freezerEffectListeners)
            freezerEffectInvoker.AddFreezerEffectListener(listener);
    }

    public static void AddFreezerEffectListener(UnityAction<float> freezerEffectListener)
    {
        // add the new listener to the list of listeners
        freezerEffectListeners.Add(freezerEffectListener);

        // ensure that this new listener is added to all existing new invokers
        foreach (PickupBlock block in freezerEffectInvokers)
            block.AddFreezerEffectListener(freezerEffectListener);
    }

    #endregion

    #region SpeedupEffect

    public static void AddSpeedupEffectInvoker(PickupBlock speedupEffectInvoker)
    {
        // add the new invoker to the list of invokers
        speedupEffectInvokers.Add(speedupEffectInvoker);

        // ensure that all existing listeners are added to this new invoker
        foreach (UnityAction<float> listener in speedupEffectListeners)
            speedupEffectInvoker.AddSpeedupEffectListener(listener);
    }

    public static void AddSpeedupEffectListener(UnityAction<float> speedupEffectListener)
    {
        // add the new listener to the list of listeners
        speedupEffectListeners.Add(speedupEffectListener);

        // ensure that this new listener is added to all existing new invokers
        foreach (PickupBlock block in speedupEffectInvokers)
            block.AddSpeedupEffectListener(speedupEffectListener);
    }

    #endregion
}
