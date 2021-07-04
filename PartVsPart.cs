using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartVsPart : MonoBehaviour
{
    Rigidbody Part;
    void Start()
    {
        Part = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Part") && Part.velocity.magnitude > 3.5f)
        {
            Part.constraints = RigidbodyConstraints.None;

        } else if (Other.CompareTag("WallPart") && Part.velocity.magnitude > 3.5f)
        {
            Part.constraints = RigidbodyConstraints.None;

        } else if (Other.CompareTag("Player") && Other.attachedRigidbody.velocity.magnitude > 3.5f)
        {
            Part.constraints = RigidbodyConstraints.None;
        }

    }
    private void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Part"))
        {

        }
    }
}
