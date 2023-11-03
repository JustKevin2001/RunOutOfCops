using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Spawner_Controller : MonoBehaviour
{
    // Attributes
    [Header("Border")]
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;

    [Header("Speed")]
    [SerializeField] float moveSpeed;
    float xSpeed;

    [Header("Player Prefabs")]
    [SerializeField] GameObject playerPrefabs;

    public List<GameObject> playerList = new List<GameObject>();

    public AudioSource playerSpawnerAudioSource;
    [SerializeField] AudioClip GateClip, SucceedClip, FailedClip; 

    bool isPlayerMoving;

    void Start()
    {
    }

    void Update()
    {

        if(!isPlayerMoving)
        {
            return;
        }

        // Work on the real devices not in the editor
        float touchX = 0;
        float newXValue = 0;

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }

        else if(Input.GetMouseButton(0))
        {
            xSpeed = 500;
            touchX = Input.GetAxis("Mouse X");
        }

        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, leftBorder, rightBorder);

        Vector3 playerMove = new Vector3(newXValue, transform.position.y, transform.position.z + Time.deltaTime * moveSpeed);
        transform.position = playerMove;
    }


    public void SpawnPlayer(int gateValue, GateType gateType)
    {
        PlayAudio(GateClip);

        if (gateType == GateType.AdditionType)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayer = Instantiate(playerPrefabs, GetPlayerPos(), Quaternion.identity, transform);
                playerList.Add(newPlayer);
            }
        }

        if(gateType == GateType.multiplyType)
        {
            int newPlayerCount = (playerList.Count * gateValue) - playerList.Count;

            for(int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayer = Instantiate(playerPrefabs, GetPlayerPos(), Quaternion.identity, transform);
                playerList.Add(newPlayer);
            }
        }
    }

    public Vector3 GetPlayerPos()
    {
        // Returns a random point inside or on a sphere with radius 1.0
        Vector3 randPos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + randPos;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FinishLine")
        {
            isPlayerMoving = false;
            StartAllPlayerIdling();
            GameManager.instance.ShowSucceedPanel();
            StopBackGroundMusic();
            PlayAudio(SucceedClip);
        }
    }

    public void CopGotKilled(GameObject copGO)
    {
        playerList.Remove(copGO);
        Destroy(copGO);
        DetectCopCount();
    }

    private void DetectCopCount()
    {
        if(playerList.Count <= 0)
        {
            StopPlayer();
            PlayAudio(FailedClip);
            GameManager.instance.ShowFailPanel();
        }
    }

    public void EnemyDetect(GameObject target)
    {
        StopPlayer();
        LookAtEnemy(target);
        StartAllPlayerShooting();
    }

    private void LookAtEnemy(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = lookRotation;
    }

    public void LookAtFoward()
    {
        transform.rotation = Quaternion.identity;
    }

    public void AllZombieKilled()
    {
        LookAtFoward();
        MovePlayer();
    }

    public void MovePlayer()
    {
        isPlayerMoving = true;
        StartAllPlayerRunningAgain();
    }

    private void StopPlayer()
    {
        isPlayerMoving = false;
    }

    private void StartAllPlayerShooting()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            PlayerController cop = playerList[i].GetComponent<PlayerController>();
            cop.StartShooting();
        }
    }

    private void StartAllPlayerRunningAgain()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            PlayerController cop = playerList[i].GetComponent<PlayerController>();
            cop.StopShooting();
        }
    }

    private void StartAllPlayerIdling()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            PlayerController cop = playerList[i].GetComponent<PlayerController>();
            cop.StartIdleAnim();
        }
    }

    private void PlayAudio(AudioClip audio)
    {
        if(playerSpawnerAudioSource != null)
        {
            playerSpawnerAudioSource.PlayOneShot(audio, .5f);
        }
    }

    private void StopBackGroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop(); 
    }
}


// drag: Luc can