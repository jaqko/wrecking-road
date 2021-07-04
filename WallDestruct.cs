using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestruct : MonoBehaviour
{
    BoxCollider Wall;
    void Start()
    {
        Wall = GetComponent<BoxCollider>();
        Wall.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrashDetection") && CrashPointCalc.Speed > 4f)
        {
            Wall.enabled = false;
        }
    }
}
