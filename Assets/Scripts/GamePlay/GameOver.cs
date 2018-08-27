using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int finalScore = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().Score;
        Text gameOverText = GameObject.FindGameObjectWithTag("FinalScoreText").GetComponent<Text>();
        gameOverText.text = "Final Score: " + finalScore;
    }
}
