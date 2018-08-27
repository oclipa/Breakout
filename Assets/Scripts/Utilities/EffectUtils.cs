using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils
{
    public static float SpeedupFactor
    {
        get { return Camera.main.gameObject.GetComponent<SpeedupEffectMonitor>().SpeedupFactor; }
    }

    public static float RemainingSpeedUpTime
    {
        get { return Camera.main.gameObject.GetComponent<SpeedupEffectMonitor>().RemainingTime; }
    }

    public static bool IsSpeededUp
    {
        get { return Camera.main.gameObject.GetComponent<SpeedupEffectMonitor>().IsSpeededUp; }
    }
}
