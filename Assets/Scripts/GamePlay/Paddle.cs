using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the paddle
/// </summary>
public class Paddle : MonoBehaviour {

    // the RidigBody2D of the paddle
    Rigidbody2D rb;

    // half the width of the paddle
    float paddleHalfWidth;

    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    bool isFrozen;
    Timer freezeTimer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        paddleHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;

        freezeTimer = gameObject.AddComponent<Timer>();
        EventManager.AddFreezerEffectListener(Freeze);
	}
	
	// Update is called once per frame
	void Update () {
        if (isFrozen && freezeTimer.Finished)
            isFrozen = false;
	}

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0f)
            {
                Vector2 position = rb.position;
                float x = position.x;
                x += horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
                position.x = CalculateClampedX(x);

                rb.MovePosition(position);
            }
        } 
    }

    private float CalculateClampedX(float x)
    {
        if (x < ScreenUtils.ScreenLeft + paddleHalfWidth)
        {
            x = ScreenUtils.ScreenLeft + paddleHalfWidth;
        }
        else if (x > ScreenUtils.ScreenRight - paddleHalfWidth)
        {
            x = ScreenUtils.ScreenRight - paddleHalfWidth;
        }

        return x;
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") && isTopCollision(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                paddleHalfWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);

            AudioManager.Play(AudioClipName.Pock);
        }
    }

    private bool isTopCollision(Collision2D coll)
    {
        ContactPoint2D contactPoint = coll.GetContact(0);
        return nearlyEqual(contactPoint.point.y, -4.12f, 0.05f); // is below the top of the paddle, i.e. a side
    }

    public static bool nearlyEqual(float a, float b, float epsilon)
    {
        return Mathf.Abs(a - b) < epsilon;
    }

    /// <summary>
    /// Freezes the paddle for the indicated duration
    /// </summary>
    /// <param name="duration">Duration.</param>
    public void Freeze(float duration)
    {
        isFrozen = true;
        if (freezeTimer.Running)
        {
            freezeTimer.Stop();
            freezeTimer.Duration += duration;
        }
        else
        {
            freezeTimer.Duration = duration;
        }
        freezeTimer.Run();
    }
}
