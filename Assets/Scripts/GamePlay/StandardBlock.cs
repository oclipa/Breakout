using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block {

    [SerializeField]
    Sprite[] sprites = new Sprite[3];

	// Use this for initialization
	override protected void Start () {
        this.points = ConfigurationUtils.StandardBlockPoints;

        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];

        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
