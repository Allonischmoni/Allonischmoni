using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUI;

    public int lastWave;

    void Start()
    {
        gameIsOver = false;
    }
    void Update()
    {
        if (PlayerStats.Lives <= 0 || PlayerStats.Rounds == PlayerStats .lastWave )
        {
            if (gameIsOver)
            {
                return;
            }
            EndGame();
        }
    }
    void EndGame()
    {
        gameIsOver = true;
        Debug.Log("GameOver");
        gameOverUI.SetActive(true);
    }
}
