using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";
    Dictionary<ConfigurationDataValueName, float> values =
        new Dictionary<ConfigurationDataValueName, float>();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return values[ConfigurationDataValueName.PaddleMoveUnitsPerSecond]; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return values[ConfigurationDataValueName.BallImpulseForce]; ; }
    }

    /// <summary>
    /// Gets the life time of the ball
    /// </summary>
    /// <value>lifetime in seconds</value>
    public float BallLifeTime
    {
        get { return values[ConfigurationDataValueName.BallLifeTime]; ; }
    }

    /// <summary>
    /// Gets the minimum time before an additional ball will be spawned
    /// </summary>
    /// <value>min spawn time</value>
    public float MinRandomSpawnTime
    {
        get { return values[ConfigurationDataValueName.MinRandomSpawnTime]; ; }
    }

    /// <summary>
    /// Gets the maximum time before an additional ball will be spawned
    /// </summary>
    /// <value>max spawn time</value>
    public float MaxRandomSpawnTime
    {
        get { return values[ConfigurationDataValueName.MaxRandomSpawnTime]; ; }
    }

    /// <summary>
    /// Gets the points for a standard block
    /// </summary>
    /// <value>standard block points</value>
    public int StandardBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.StandardBlockPoints]; ; }
    }

    /// <summary>
    /// Gets the points for a bonus block
    /// </summary>
    /// <value>bonus block points</value>
    public int BonusBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.BonusBlockPoints]; ; }
    }

    /// <summary>
    /// Gets the points for a pickup block
    /// </summary>
    /// <value>pickup block points</value>
    public int PickupBlockPoints
    {
        get { return (int)values[ConfigurationDataValueName.PickupBlockPoints]; ; }
    }

    /// <summary>
    /// Probability of getting a standard block
    /// </summary>
    /// <value>probablility</value>
    public int StandardBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.StandardBlockProbability]; ; }
    }

    /// <summary>
    /// Probability of getting a bonus block
    /// </summary>
    /// <value>probablility</value>
    public int BonusBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.BonusBlockProbability]; ; }
    }

    /// <summary>
    /// Probability of getting a freezer block
    /// </summary>
    /// <value>probablility</value>
    public int FreezerBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.FreezerBlockProbability]; ; }
    }

    /// <summary>
    /// Probability of getting a speedup block
    /// </summary>
    /// <value>probablility</value>
    public int SpeedupBlockProbability
    {
        get { return (int)values[ConfigurationDataValueName.SpeedupBlockProbability]; ; }
    }

    /// <summary>
    /// Number of balls per game
    /// </summary>
    /// <value>number of balls</value>
    public int BallsPerGame
    {
        get { return (int)values[ConfigurationDataValueName.BallsPerGame]; ; }
    }

    /// <summary>
    /// The length of time the paddle is frozen when a FreezerBlock is picked up
    /// </summary>
    /// <value>duration</value>
    public float FreezeDuration
    {
        get { return values[ConfigurationDataValueName.FreezeDuration]; ; }
    }

    /// <summary>
    /// The length of time the ball speeds up for when a SpeedupBlock is picked up
    /// </summary>
    /// <value>duration</value>
    public float SpeedupDuration
    {
        get { return values[ConfigurationDataValueName.SpeedupDuration]; ; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader streamReader = null;
        try
        {
            ReadFromConfigurationFile(streamReader, values);
        }
        catch (Exception x)
        {
            Debug.Log("Whoops: " + x.Message);
            SetDefaults(values);
        }
        finally
        {
            if (streamReader != null)
                streamReader.Close();
        }
    }

    /// <summary>
    /// Reads from configuration file.
    /// </summary>
    /// <param name="streamReader">Stream reader.</param>
    /// <param name="values">The dictionary of values</param>
    static void ReadFromConfigurationFile(StreamReader streamReader, Dictionary<ConfigurationDataValueName, float> values)
    {
        streamReader = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
        string currentLine = streamReader.ReadLine();
        while (currentLine != null)
        {
            string[] tokens = currentLine.Split(',');
            ConfigurationDataValueName valueName =
                (ConfigurationDataValueName)Enum.Parse(
                    typeof(ConfigurationDataValueName), tokens[0]);

            float val = float.Parse(tokens[1]);
            if (values.ContainsKey(valueName))
            {
                values[valueName] = val;
            }
            else
            {
                values.Add(valueName, val);
            }

            currentLine = streamReader.ReadLine();
        }
    }

    #endregion

    /// <summary>
    /// Sets defaults values for the configuration options
    /// </summary>
    /// <param name="values">The dictionary of values.</param>
    static void SetDefaults(Dictionary<ConfigurationDataValueName, float> values)
    {
        if (values != null)
        {
            values.Clear();
            values.Add(ConfigurationDataValueName.BallImpulseForce, 4f);
            values.Add(ConfigurationDataValueName.BallLifeTime, 10f);
            values.Add(ConfigurationDataValueName.BallsPerGame, 40f);
            values.Add(ConfigurationDataValueName.BonusBlockPoints, 2f);
            values.Add(ConfigurationDataValueName.BonusBlockProbability, 10f);
            values.Add(ConfigurationDataValueName.FreezeDuration, 2f);
            values.Add(ConfigurationDataValueName.FreezerBlockProbability, 5f);
            values.Add(ConfigurationDataValueName.MaxRandomSpawnTime, 10f);
            values.Add(ConfigurationDataValueName.MinRandomSpawnTime, 5f);
            values.Add(ConfigurationDataValueName.PaddleMoveUnitsPerSecond, 8f);
            values.Add(ConfigurationDataValueName.PickupBlockPoints, 3f);
            values.Add(ConfigurationDataValueName.SpeedupBlockProbability, 5f);
            values.Add(ConfigurationDataValueName.SpeedupDuration, 2f);
            values.Add(ConfigurationDataValueName.StandardBlockPoints, 10f);
            values.Add(ConfigurationDataValueName.StandardBlockProbability, 80f);
        }
    }
}
