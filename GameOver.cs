using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text winningText;
    public string win = "You won!";
    public string loose = "You loose!";
    public string MenuToLoad = "MainMenu";

    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
        if (PlayerStats .Rounds >= PlayerStats .lastWave)
        {
            winningText.text = win;
        }
        else
        {
            winningText.text = loose;
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(MenuToLoad);
    }
}
