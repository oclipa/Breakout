using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the ball
/// </summary>
public class Ball : IntEventInvoker
{
    Rigidbody2D rb;

    Timer timerBallLifeTime;
    Timer timerBallStart;
    bool isStarted;

    bool isSpeededup;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();

        timerBallLifeTime = gameObject.AddComponent<Timer>();
        timerBallLifeTime.Duration = ConfigurationUtils.BallLifeTime;
        timerBallLifeTime.Run();

        timerBallStart = gameObject.AddComponent<Timer>();
        timerBallStart.Duration = 1;
        timerBallStart.Run();

        intUnityEvents.Add(EventName.SubtractBallsEvent, new SubtractBallsEvent());
        IntEventManager.AddInvoker(EventName.SubtractBallsEvent, this);
        intUnityEvents.Add(EventName.BallsDiedEvent, new BallsDiedEvent());
        IntEventManager.AddInvoker(EventName.BallsDiedEvent, this);
    }

    private void moveBall(float impulseForce)
    {
        float angleInRadians = 270 * Mathf.Deg2Rad;

        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        rb.AddForce(moveDirection * impulseForce, ForceMode2D.Impulse);
    }

	// Update is called once per frame
	void Update () {

        if (!isStarted && timerBallStart.Finished)
        {
            moveBall(ConfigurationUtils.BallImpulseForce);
            isStarted = true;
        }

        if (rb != null && isStarted)
        {
            //Debug.Log(this.name + " IsSpeedUp: " + EffectUtils.IsSpeededUp);
            if (isSpeededup && EffectUtils.RemainingSpeedUpTime <= 0)
            {
                //Debug.Log(this.name + " Slowdown");
                Vector2 velocity = rb.velocity;
                velocity.x = velocity.x / EffectUtils.SpeedupFactor;
                velocity.y = velocity.y / EffectUtils.SpeedupFactor;
                rb.velocity = velocity;
                isSpeededup = false;
            }
            else if (!isSpeededup && EffectUtils.RemainingSpeedUpTime > 0)
            {
                //Debug.Log(this.name + " Speedup");
                Vector2 velocity = rb.velocity;
                velocity.x = velocity.x * EffectUtils.SpeedupFactor;
                velocity.y = velocity.y * EffectUtils.SpeedupFactor;
                rb.velocity = velocity;
                isSpeededup = true;
            }
        }

        if (timerBallLifeTime.Finished)
        {
            Destroy(gameObject);
            //Camera.main.GetComponent<BallSpawner>().SpawnBall();
            intUnityEvents[EventName.BallsDiedEvent].Invoke(1);
            disableEvents();
        }
	}

    private void FixedUpdate()
    {
        if (timerBallLifeTime.Remaining < 2)
        {
            // Reduces the transparancy of the ball as it approaches death
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a -= 0.01f;
            spriteRenderer.color = color;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (rb != null)
        {
            float speed = rb.velocity.magnitude;

            rb.velocity = speed * direction;
        }
    }

    private void OnBecameInvisible()
    {
        if (!timerBallLifeTime.Finished)
        {
            if (transform != null)
            {
                if (transform.position.y < ScreenUtils.ScreenBottom)
                {
                    AudioManager.Play(AudioClipName.LostBall);
                    Destroy(gameObject);
                }
            }
        }
        intUnityEvents[EventName.SubtractBallsEvent].Invoke(1);
        disableEvents();
    }

    private void disableEvents()
    {
        this.RemoveAllListeners(EventName.BallsDiedEvent);
        this.RemoveAllListeners(EventName.SubtractBallsEvent);
        IntEventManager.RemoveInvoker(EventName.BallsDiedEvent, this);
        IntEventManager.RemoveInvoker(EventName.SubtractBallsEvent, this);
    }
}
