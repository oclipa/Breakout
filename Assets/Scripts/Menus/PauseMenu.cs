using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0; // pauses the game
	}
	
	public void HandleResumeButtonOnClickEvent() 
    {
        AudioManager.Play(AudioClipName.Click);
        Time.timeScale = 1; // restarts the game
        Destroy(gameObject);
	}

    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Click);
        Time.timeScale = 1; // ensure that the game can run if restarted
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
