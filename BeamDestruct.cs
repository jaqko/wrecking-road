using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDestruct : MonoBehaviour
{
    BeamDestruct activatescript;
    public BoxCollider pointDisable;
    public Rigidbody car;
    public Rigidbody beam;
    public Rigidbody part1, part2, part3, part4;
    void Start()
    {
        activatescript = GetComponent<BeamDestruct>();
        activatescript.enabled = true;
        pointDisable.enabled = true;
        beam.constraints = RigidbodyConstraints.FreezeAll;
        part1.constraints = RigidbodyConstraints.FreezeAll; part2.constraints = RigidbodyConstraints.FreezeAll; part3.constraints = RigidbodyConstraints.FreezeAll;  part4.constraints = RigidbodyConstraints.FreezeAll ;
    }

    // Update is called once per frame
    void Update()
    {
        if (part1.constraints == RigidbodyConstraints.None || part2.constraints == RigidbodyConstraints.None)
        {
            pointDisable.enabled = false;
            beam.constraints = RigidbodyConstraints.None;
            part1.constraints = RigidbodyConstraints.None; part2.constraints = RigidbodyConstraints.None; part3.constraints = RigidbodyConstraints.None; part4.constraints = RigidbodyConstraints.FreezePositionY;
        } if (part3.constraints == RigidbodyConstraints.None)
        {
            pointDisable.enabled = false;
            beam.constraints = RigidbodyConstraints.None;
        }
        if (part4.name == "Placeholder")
        {
            part4.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrashDetection") && CrashPointCalc.Speed > 2f)
        {
            pointDisable.enabled = false;
            beam.constraints = RigidbodyConstraints.None;
            part1.constraints = RigidbodyConstraints.None; part2.constraints = RigidbodyConstraints.None; part3.constraints = RigidbodyConstraints.None; part4.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}
