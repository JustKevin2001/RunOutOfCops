using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject playerSpawnerGO;
    public ZombieSpawnerController zombieSpawnerController;
    bool isZombieAlive;

    void Start()
    {
        isZombieAlive = true;    
    }

    private void FixedUpdate()
    {
        if(zombieSpawnerController.isZombieAttack == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerSpawnerGO.transform.position, Time.fixedDeltaTime * 1.5f);
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && isZombieAlive == true)
        {
            isZombieAlive = false;
            zombieSpawnerController.ZombieAttackThisCop(other.gameObject, gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            zombieSpawnerController.ZombieGotShoot(gameObject);
            Destroy(other.gameObject);
        }
    }
}
