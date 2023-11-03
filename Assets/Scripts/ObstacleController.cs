using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Attributes
    private Player_Spawner_Controller player_Spawner_Controller;
    private GameObject playerSpawnerGO;

    void Start()
    {
        // Tim game object trong editor co tag nay
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        // Tu game object do de get component
        player_Spawner_Controller = playerSpawnerGO.GetComponent<Player_Spawner_Controller>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player_Spawner_Controller.CopGotKilled(other.gameObject);
        }
    }
}
