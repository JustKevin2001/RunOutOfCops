using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GateType { multiplyType, AdditionType }
public class GatesController : MonoBehaviour
{
    // Attibutes
    [SerializeField] int gateValue;
    private bool hasGateUsed;

    private Player_Spawner_Controller playerSpawnerInScript;
    private GameObject playerSpawnerGO;
    private GatesHolderController gateHolder;

    [SerializeField] TextMeshProUGUI gatePoint;
    [SerializeField] GateType gateType;

    private void Awake()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerInScript = playerSpawnerGO.GetComponent<Player_Spawner_Controller>();  
        gateHolder = transform.parent.gameObject.GetComponent<GatesHolderController>();
    }


    void Start()
    {
        AddGateValueAddSymbol();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasGateUsed)
        {
            // Spawn new cops
            hasGateUsed = true;
            playerSpawnerInScript.SpawnPlayer(gateValue, gateType);
            gateHolder.CloseGates();
            Destroy(gameObject);    
        }
    }

    private void AddGateValueAddSymbol()
    {
        switch(gateType)
        {
            case GateType.multiplyType:
                gatePoint.text = "x" + gateValue.ToString();
                break;
            case GateType.AdditionType:
                gatePoint.text = "+" + gateValue.ToString();
                break;
            default:
                break;

        }
    }
}
