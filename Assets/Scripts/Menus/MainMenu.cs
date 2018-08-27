using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void HandlePlayButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Click);
        IntEventManager.Initialize();
        SceneManager.LoadScene("Gameplay");
    }

    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Click);
        MenuManager.GoToMenu(MenuName.Quit);
    }

    public void HandleHelpButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Click);
        GameObject mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);
        else
            Debug.Log("MainMenuCanvas was null");
        Object.Instantiate(Resources.Load("HelpMenu"));
    }

    public void HandleBackButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.Click);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
