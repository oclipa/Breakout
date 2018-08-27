using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IntEventManager.AddListener(EventName.GameOverEvent, GameOver);
    }

    void GameOver(int score)
    {
        AudioManager.Play(AudioClipName.GameOver);

        MenuManager.GoToMenu(MenuName.GameOver);
    }
}
