using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    void Start()
    {
        
    }


    // LateUpdate still call in every frame but after Update
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
