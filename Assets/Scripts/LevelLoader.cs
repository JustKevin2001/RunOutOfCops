using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Attributes
    public static LevelLoader instance;
    private int maxLevel;
    private int currentlevel;

    private void Awake()
    {
        MakeInstance();
        maxLevel = 2;
        DontDestroyOnLoad(this.gameObject);
        GetLevel();
    }

    void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void NextLevel()
    {
        currentlevel++;
        if(currentlevel > maxLevel)
        {
            currentlevel = 1;
        }

        PlayerPrefs.SetInt("KeyLevel", currentlevel);
        LoadLevel();
    }

    public void GetLevel()
    {
        currentlevel = PlayerPrefs.GetInt("keyLevel", 1);
        LoadLevel();
    }

    private void LoadLevel()
    {
        string levelName = "Level" + currentlevel;
        SceneManager.LoadScene(levelName);
    }
}
