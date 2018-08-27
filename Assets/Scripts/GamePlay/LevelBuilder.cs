using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
    
    [SerializeField]
    Paddle prefabPaddle;
    [SerializeField]
    StandardBlock prefabStandardBlock;
    [SerializeField]
    BonusBlock prefabBonusBlock;
    [SerializeField]
    PickupBlock prefabPickupBlock;

    float xStart = -8.8f;
    float yStart = 1.3f;
    float inc = 0.7f;
    static int numOfRows = 3; // 3
    static int numOfCols = 24; // 24

    int standardBlockProbability;
    int bonusBlockProbability;
    int freezerBlockProbability;
    int speedupBlockProbability;

	// Use this for initialization
	void Start () {
        Instantiate<Paddle>(prefabPaddle);

        standardBlockProbability = ConfigurationUtils.StandardBlockProbability;
        bonusBlockProbability = standardBlockProbability + ConfigurationUtils.BonusBlockProbability;
        freezerBlockProbability = bonusBlockProbability + ConfigurationUtils.FreezerBlockProbability;
        speedupBlockProbability = freezerBlockProbability + ConfigurationUtils.SpeedupBlockProbability;

        float x = xStart;
        float y = yStart;

        for (int row = 0; row < numOfRows; row++)
        {
            for (int col = 0; col < numOfCols; col++)
            {
                x = x + inc;
                PlaceBlock(new Vector3(x, y, 0));
            }
            x = xStart;
            y = y + inc;
        }
        //PlaceBlock(new Vector3(0.35f, 2, 0));
        //PlaceBlock(new Vector3(-0.35f, 2, 0));
        //PlaceBlock(new Vector3(0, 4, 0));
    }

    private void PlaceBlock(Vector3 position)
    {
        int blockType = Random.Range(0, 100);

        if (blockType <= standardBlockProbability)
        {
            Instantiate<StandardBlock>(prefabStandardBlock, position, Quaternion.identity);
        } 
        else if (blockType > standardBlockProbability && blockType <= bonusBlockProbability)
        {
            Instantiate<BonusBlock>(prefabBonusBlock, position, Quaternion.identity);
        }
        else if (blockType > bonusBlockProbability && blockType <= freezerBlockProbability)
        {
            PickupBlock block = Instantiate<PickupBlock>(prefabPickupBlock, position, Quaternion.identity);
            block.PickupEffect = PickupEffect.Freezer;
        }
        else if (blockType > freezerBlockProbability && blockType <= speedupBlockProbability)
        {
            PickupBlock block = Instantiate<PickupBlock>(prefabPickupBlock, position, Quaternion.identity);
            block.PickupEffect = PickupEffect.Speedup;
        }
    }

    /// <summary>
    /// Gets the block count.
    /// </summary>
    /// <value>The block count.</value>
    public static int BlockCount
    {
        get { return numOfCols * numOfRows; }
    }
}
