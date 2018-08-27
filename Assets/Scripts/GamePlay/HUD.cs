using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : IntEventInvoker {

    Text scoreText;
    int score;

    static Text ballsLeftText;
    static int ballsLeft;
    static int blockCount;

    bool gameOver;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        ballsLeftText = GameObject.FindGameObjectWithTag("BallsLeftText").GetComponent<Text>();
        ballsLeft = ConfigurationUtils.BallsPerGame;
        ballsLeftText.text = "Balls Left: " + ballsLeft.ToString();

        blockCount = LevelBuilder.BlockCount; // cheap 'n nasty; could also get this by finding tagged block count

        IntEventManager.AddListener(EventName.PointsAdded, AddPoints);
        IntEventManager.AddListener(EventName.SubtractBallsEvent, SubtractBalls);
        IntEventManager.AddListener(EventName.BlockDestroyed, SubtractBlocks);

        intUnityEvents.Add(EventName.GameOverEvent, new GameOverEvent());
        IntEventManager.AddInvoker(EventName.GameOverEvent, this);
    }

    /// <summary>
    /// Adds points to the current score
    /// </summary>
    /// <param name="points">Points.</param>
    public void AddPoints(int points)
    {
        if (scoreText != null) // prevent access of destroyed Text object
        {
            score += points;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    /// <summary>
    /// Subtract the number of balls used from the remaining ball count.
    /// </summary>
    /// <param name="balls">Balls.</param>
    public void SubtractBalls(int balls)
    {
        if (!gameOver)
        {
            if (ballsLeftText != null) // prevent access of destroyed Text object
            {
                ballsLeft -= balls;
                ballsLeftText.text = "Balls Left: " + ballsLeft.ToString();
            }

            if (ballsLeft <= 0)
            {
                intUnityEvents[EventName.GameOverEvent].Invoke(score);
                gameOver = true;
            }
        }
    }


    /// <summary>
    /// Subtract the number of destroyed blocks from the remaining block count.
    /// </summary>
    /// <param name="balls">Balls.</param>
    public void SubtractBlocks(int blocks)
    {
        if (!gameOver)
        {
            blockCount -= blocks;

            if (blockCount <= 0)
            {
                intUnityEvents[EventName.GameOverEvent].Invoke(score);
                gameOver = true;
            }
        }
    }

    /// <summary>
    /// Gets the score.
    /// </summary>
    /// <value>The score.</value>
    public int Score
    {
        get 
        {
            return this.score;
        }
    }
}
