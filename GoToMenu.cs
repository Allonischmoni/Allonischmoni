using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public string MenuToLoad = "MainMenu";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MenuToLoad);
    }
}
