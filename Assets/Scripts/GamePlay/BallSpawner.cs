using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    [SerializeField]
    Ball prefabBall;

    Timer timerRandomBallSpawn;

    float minBallSpawnTime;
    float maxBallSpawnTime;

    bool retrySpawn;
    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;

    int ballId = 0;

    bool gameOver;

	// Use this for initialization
	void Start () {
        // set the min/max spawn times for random balls
        minBallSpawnTime = ConfigurationUtils.MinRandomSpawnTime;
        maxBallSpawnTime = ConfigurationUtils.MaxRandomSpawnTime;

        // get the boundaries of the spawn location
        Ball tempBall = Instantiate<Ball>(prefabBall);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;
        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);
        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);
        Destroy(tempBall.gameObject);

        // create a time to spawn a ball at random intervals
        timerRandomBallSpawn = gameObject.AddComponent<Timer>();
        
        retrySpawn = true;

        // Add listeners for events
        IntEventManager.AddListener(EventName.SubtractBallsEvent, SpawnBalls);
        IntEventManager.AddListener(EventName.BallsDiedEvent, SpawnBalls);
        IntEventManager.AddListener(EventName.GameOverEvent, GameOver);
    }

    // Update is called once per frame
    void Update () {
        // If the timer for the random spawn has finished, or we 
        // need to retry the spawn, try spawning a new ball now
        if (!gameOver && (timerRandomBallSpawn.Finished || retrySpawn))
        {
            SpawnBall();
            startSpawnTimer();
        }
	}

    /// <summary>
    /// Spawns a specified number of balls.
    /// </summary>
    /// <param name="numOfBalls">Number of balls.</param>
    private void SpawnBalls(int numOfBalls)
    {
        if (!gameOver)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                SpawnBall();
            }
        }
    }

    /// <summary>
    /// Spawns a single ball
    /// </summary>
    private void SpawnBall()
    {
        if (!gameOver)
        {
            // check if a new ball would overlap an existing ball (if so, retry later)
            if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
            {
                retrySpawn = false;
                Ball ball = Instantiate<Ball>(prefabBall);
                ball.name = ballId.ToString(); // helps with debugging
                ballId++;
                startSpawnTimer();
            }
            else
            {
                retrySpawn = true;
            }
        }
    }

    /// <summary>
    /// Starts the spawn timer for the next random ball
    /// </summary>
    private void startSpawnTimer()
    {
        timerRandomBallSpawn.Duration = Random.Range(minBallSpawnTime, maxBallSpawnTime);
        timerRandomBallSpawn.Run();
    }

    // Ensure we stop spawning balls when the game ends
    void GameOver(int score)
    {
        gameOver = true;
    }
}
