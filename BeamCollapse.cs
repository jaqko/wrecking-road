using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollapse : MonoBehaviour
{
    public Rigidbody C1; public Rigidbody C2; public Rigidbody C3; public Rigidbody C4;
    void Start()
    {
        C1.constraints = RigidbodyConstraints.FreezeAll; C2.constraints = RigidbodyConstraints.FreezeAll; C3.constraints = RigidbodyConstraints.FreezeAll; C4.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrashDetection") && other.attachedRigidbody.velocity.magnitude > 3.5f)
        {
            C1.constraints = RigidbodyConstraints.None; C2.constraints = RigidbodyConstraints.None; C3.constraints = RigidbodyConstraints.None; C4.constraints = RigidbodyConstraints.None;
            StartCoroutine(Wait1(2.5f));
        }
    }
    public IEnumerator Wait1(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(C1.gameObject);
        Destroy(C2.gameObject);
        Destroy(C3.gameObject);
        Destroy(C4.gameObject);
    }
}
