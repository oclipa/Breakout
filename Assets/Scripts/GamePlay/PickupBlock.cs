using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupBlock : Block {

    [SerializeField]
    Sprite freezerSprite;
    [SerializeField]
    Sprite speedupSprite;

    PickupEffect pickupEffect;

    float freezeDuration;
    FreezerEffectActivated freezerEventActivated;

    float speedupDuration;
    SpeedupEffectActivated speedupEventActivated;

    // Use this for initialization
    override protected void Start()
    {
        this.points = ConfigurationUtils.PickupBlockPoints;

        base.Start();
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    /// <summary>
    /// The pickup effect.
    /// </summary>
    public PickupEffect PickupEffect
    {
        get
        {
            return this.pickupEffect; 
        }
        set
        {
            this.pickupEffect = value;

            switch(this.pickupEffect)
            {
                case PickupEffect.Freezer:
                    GetComponent<SpriteRenderer>().sprite = freezerSprite;
                    this.freezeDuration = ConfigurationUtils.FreezeDuration;
                    this.freezerEventActivated = new FreezerEffectActivated();
                    EventManager.AddFreezerEffectInvoker(this);
                    break;
                case PickupEffect.Speedup:
                    GetComponent<SpriteRenderer>().sprite = speedupSprite;
                    this.speedupDuration = ConfigurationUtils.SpeedupDuration;
                    this.speedupEventActivated = new SpeedupEffectActivated();
                    EventManager.AddSpeedupEffectInvoker(this);
                    break;
                default:
                    GetComponent<SpriteRenderer>().sprite = freezerSprite;
                    break;
            }
        }
    }

    /// <summary>
    /// Adds an event listener for the freezer effect
    /// </summary>
    /// <param name="listener">Listener.</param>
    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEventActivated.AddListener(listener);
    }

    /// <summary>
    /// Adds an event listener for the speedup effect
    /// </summary>
    /// <param name="listener">Listener.</param>
    public void AddSpeedupEffectListener(UnityAction<float> listener)
    {
        speedupEventActivated.AddListener(listener);
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        if (this.pickupEffect == PickupEffect.Freezer)
        {
            AudioManager.Play(AudioClipName.Freeze);
            freezerEventActivated.Invoke(this.freezeDuration);
        }

        if (this.pickupEffect == PickupEffect.Speedup)
        {
            AudioManager.Play(AudioClipName.Speedup);
            speedupEventActivated.Invoke(this.speedupDuration);
        }

        base.OnCollisionEnter2D(coll);
    }
}
