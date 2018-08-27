using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block {

    [SerializeField]
    Sprite sprite;

    // Use this for initialization
    override protected void Start()
    {
        this.points = ConfigurationUtils.BonusBlockPoints;

        GetComponent<SpriteRenderer>().sprite = sprite;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
