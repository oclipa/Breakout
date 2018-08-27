using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configurationData;

    #region Properties
    
    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return configurationData.PaddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force for the ball
    /// </summary>
    /// <value>ball impulse force</value>
    public static float BallImpulseForce
    {
        get { return configurationData.BallImpulseForce; }
    }

    /// <summary>
    /// Gets the life time of the ball
    /// </summary>
    /// <value>life time  in seconds</value>
    public static float BallLifeTime
    {
        get { return configurationData.BallLifeTime; }
    }

    /// <summary>
    /// Gets the minimum time before an additional ball will be spawned
    /// </summary>
    /// <value>min spawn time</value>
    public static float MinRandomSpawnTime
    {
        get { return configurationData.MinRandomSpawnTime; }
    }

    /// <summary>
    /// Gets the maximum time before an additional ball will be spawned
    /// </summary>
    /// <value>max spawn time</value>
    public static float MaxRandomSpawnTime
    {
        get { return configurationData.MaxRandomSpawnTime; }
    }

    /// <summary>
    /// Gets the points for a standard block
    /// </summary>
    /// <value>standard block points</value>
    public static int StandardBlockPoints
    {
        get { return configurationData.StandardBlockPoints; }
    }

    /// <summary>
    /// Gets the points for a bonus block
    /// </summary>
    /// <value>bonus block points</value>
    public static int BonusBlockPoints
    {
        get { return configurationData.BonusBlockPoints; }
    }

    /// <summary>
    /// Gets the points for a pickup block
    /// </summary>
    /// <value>pickup block points</value>
    public static int PickupBlockPoints
    {
        get { return configurationData.PickupBlockPoints; }
    }

    /// <summary>
    /// Probability of getting a standard block
    /// </summary>
    /// <value>probablility</value>
    public static int StandardBlockProbability
    {
        get { return configurationData.StandardBlockProbability; }
    }

    /// <summary>
    /// Probability of getting a bonus block
    /// </summary>
    /// <value>probablility</value>
    public static int BonusBlockProbability
    {
        get { return configurationData.BonusBlockProbability; }
    }

    /// <summary>
    /// Probability of getting a freezer block
    /// </summary>
    /// <value>probablility</value>
    public static int FreezerBlockProbability
    {
        get { return configurationData.FreezerBlockProbability; }
    }

    /// <summary>
    /// Probability of getting a speedup block
    /// </summary>
    /// <value>probablility</value>
    public static int SpeedupBlockProbability
    {
        get { return configurationData.SpeedupBlockProbability; }
    }

    /// <summary>
    /// Number of balls per game
    /// </summary>
    /// <value>number of balls</value>
    public static int BallsPerGame
    {
        get { return configurationData.BallsPerGame; }
    }

    /// <summary>
    /// The length of time the paddle is frozen when a FreezerBlock is picked up
    /// </summary>
    /// <value>duration</value>
    public static float FreezeDuration
    {
        get { return configurationData.FreezeDuration; }
    }

    /// <summary>
    /// The length of time the ball speeds up for when a SpeedupBlock is picked up
    /// </summary>
    /// <value>duration</value>
    public static float SpeedupDuration
    {
        get { return configurationData.SpeedupDuration; }
    }

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
