using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollapse : MonoBehaviour
{
    public Rigidbody Brick;
    public LayerMask BrickMask;
    bool NoGoingBack = false;
    
    void Start()
    {
        Brick.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down * 0.14011885f, out hit, 0.14011885f, BrickMask))
        {
            if (NoGoingBack == true)
            {
                StartCoroutine(Wait(2.5f));
            }
            else
            {
                Brick.constraints = RigidbodyConstraints.FreezeAll;
            }
        } else
        {
            Brick.constraints = RigidbodyConstraints.None;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrashDetection") && other.attachedRigidbody.velocity.magnitude > 3.5f)
        {
            Brick.constraints = RigidbodyConstraints.None;
            NoGoingBack = true;
            this.tag.Replace("WallPart", "Dead");
        }
    }
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(Brick.gameObject);
    }
}
