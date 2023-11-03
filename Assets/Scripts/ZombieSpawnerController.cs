using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] List<GameObject> zombieList = new List<GameObject>();
    private Player_Spawner_Controller playerSpawner;
    private GameObject playerSpawnerGO;
    public bool isZombieAttack;
    [SerializeField] int zombieCount;

    private void Awake()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = playerSpawnerGO.GetComponent<Player_Spawner_Controller>();
        isZombieAttack = false;
    }

    void Start()
    {
        SpawnZombie(zombieCount);
    }

    void Update()
    {
        
    }

    public void SpawnZombie(int zombieCount)
    {
        for(int i = 0; i < zombieCount; i++)
        {
            Quaternion zombieRotation = Quaternion.Euler(new Vector3(0,180, 0));
            GameObject zombieGO = Instantiate(zombie, GetZombiePosition(), zombieRotation, transform);
            ZombieController zombieController = zombieGO.GetComponent<ZombieController>();
            zombieController.playerSpawnerGO = playerSpawnerGO;
            zombieController.zombieSpawnerController = this;
            zombieList.Add(zombieGO);
        }
    }

    public Vector3 GetZombiePosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = pos + transform.position;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<BoxCollider>().enabled = false;
            playerSpawner.EnemyDetect(gameObject);
            LookAtPlayer(other.gameObject);
            isZombieAttack = true;
        }
    }

    private void LookAtPlayer(GameObject target)
    {
        Vector3 dir = transform.position - target.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);

        lookRot.x = 0;
        lookRot.z = 0;

        transform.rotation = lookRot;
    }

    public void ZombieAttackThisCop(GameObject cop, GameObject zombie)
    {
        zombieList.Remove(zombie);
        CheckZombieCount();
        playerSpawner.CopGotKilled(cop);
    }

    public void ZombieGotShoot(GameObject zombie)
    {
        zombieList.Remove(zombie);
        Destroy(zombie);
        CheckZombieCount();
    }


    public void CheckZombieCount()
    {
        if(zombieList.Count <= 0)
        {
            playerSpawner.AllZombieKilled();
        }
    }
}
