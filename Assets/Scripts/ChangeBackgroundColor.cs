using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    // Attributes
    [SerializeField] Color[] backgroundColors;
    
    void Start()
    {
        StartCoroutine("ChangeColor");
    }
    
    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(5f);

        int randomNumber = Random.Range(0, backgroundColors.Length);

        Camera.main.backgroundColor = backgroundColors[randomNumber];
    }
}
