﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    /// <summary>
    /// Gos to menu.
    /// </summary>
    /// <param name="menuName">Menu name.</param>
    public static void GoToMenu(MenuName menuName)
    {
        switch (menuName)
        {
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Pause:
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
            case MenuName.Quit:
                Application.Quit();
                break;
            case MenuName.GameOver:
                Object.Instantiate(Resources.Load("GameOverMenu"));
                break;
        }
    }
}
