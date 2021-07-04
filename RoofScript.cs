using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
    public Rigidbody Roof;
    public Rigidbody BeamL;
    public Rigidbody BeamR;
    public Rigidbody Roof1; public Rigidbody Roof2; public Rigidbody Roof3;
    public static int NoMore = 0;
    void Start()
    {
        Roof.constraints = RigidbodyConstraints.FreezeAll;
        Roof1.constraints = RigidbodyConstraints.FreezeAll; Roof2.constraints = RigidbodyConstraints.FreezeAll; Roof3.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (BeamL.constraints == RigidbodyConstraints.FreezePositionY && BeamR.constraints == RigidbodyConstraints.FreezePositionY && NoMore == 0)
        {
            
            Roof.constraints = RigidbodyConstraints.None; Roof1.constraints = RigidbodyConstraints.None; Roof2.constraints = RigidbodyConstraints.None; Roof3.constraints = RigidbodyConstraints.None;
            NoMore = 1;
        }
    }
}
