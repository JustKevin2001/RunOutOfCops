using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject successPanel;

    private void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;    
        }
    }

    public void StartButtonTapped()
    {
        mainPanel.SetActive(false);
        GameObject playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        Player_Spawner_Controller playerSpawner = playerSpawnerGO.GetComponent<Player_Spawner_Controller>();
        playerSpawner.MovePlayer();
    }

    public void ShowFailPanel()
    {
        failPanel.SetActive(true);  
    }

    public void RestartButtonTapped()
    {
        LevelLoader.instance.GetLevel();
    }

    public void ShowSucceedPanel()
    {
        successPanel.SetActive(true);
    }

    public void NextLevelButtonTapped()
    {
        LevelLoader.instance.NextLevel();
    }
}
