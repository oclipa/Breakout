using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupEffectMonitor : MonoBehaviour {

    Timer speedupEffectTimer;
    bool isSpeededUp;

    // Use this for initialization
    void Start()
    {
        speedupEffectTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedupEffectListener(Speedup);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Speedup(float duration)
    {
        //Debug.Log("Speedup requested");
        if (speedupEffectTimer.Running)
        {
            //Debug.Log("Extend duration: " + duration);
            speedupEffectTimer.Stop();
            speedupEffectTimer.Duration += duration;
            speedupEffectTimer.Run();
        }
        else
        {
            //Debug.Log("New timer: " + duration);
            speedupEffectTimer.Duration = duration;
            speedupEffectTimer.Run();
        }
    }

    public float SpeedupFactor
    {
        get { return 2.0f; }
    }

    public float RemainingTime
    {
        get { return speedupEffectTimer.Remaining; }
    }

    public bool IsSpeededUp
    {
        get 
        { 
            return speedupEffectTimer.Finished; 
        }
    }
}
