using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject mapUI;
    public GameObject startUI;
    public string scene = null ;
    public int mN = 1;

    public void SetActive()
    {
        mapUI.SetActive(!mapUI.activeSelf);
        startUI.SetActive(!startUI.activeSelf);
    }
    public void LoadTheForest()
    {
        scene  = "Level1";
        LoadScene(scene);
    }
    public void LoadLavaRiver()
    {
        scene = "Level2";
        LoadScene(scene);
    }
    public void LoadScene(string sceneToLoad)
    {
        //PlayerStats.lastWave = 2;
        if (mN == 0)
        {
            PlayerStats.lastWave = 5;
            PlayerStats.Money = 300;
            PlayerStats.Lives = 1;
        }
        if (mN == 1)
        {
            PlayerStats.lastWave = 4;
            PlayerStats.Money = 400;
            PlayerStats.Lives = 10;
        }
        if (mN == 2)
        {
            PlayerStats.lastWave = 3;
            PlayerStats.Money = 500;
            PlayerStats.Lives = 20;
        }
        mN = 1;

        SceneManager.LoadScene(sceneToLoad);
    }
    public void SetGameMode(int modeNumber)
    {
        Debug.Log(modeNumber);
        mN = modeNumber;
        
    }
}
