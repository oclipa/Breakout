using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles a block
/// </summary>
public class Block : IntEventInvoker {

    protected int points;

	// Use this for initialization
	virtual protected void Start () {

        // add self as event invoker
        intUnityEvents.Add(EventName.PointsAdded, new PointsAddedEvent());
        IntEventManager.AddInvoker(EventName.PointsAdded, this);
        intUnityEvents.Add(EventName.BlockDestroyed, new BlockDestroyedEvent());
        IntEventManager.AddInvoker(EventName.BlockDestroyed, this);
    }

    // Update is called once per frame
    void Update () {
		
	}

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            intUnityEvents[EventName.PointsAdded].Invoke(points);
            AudioManager.Play(AudioClipName.Pock);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        intUnityEvents[EventName.BlockDestroyed].Invoke(1);
        IntEventManager.RemoveInvoker(EventName.PointsAdded, this);
    }
}
