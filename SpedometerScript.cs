using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpedometerScript : MonoBehaviour
{
    public Rigidbody grabCarSpeed;
    private GameObject FindCar;
    Text MPHSpeed;
    public static bool FartOrNo = false;
    void Start()
    {
        MPHSpeed = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float MiddleManFloat = grabCarSpeed.velocity.magnitude * (Mathf.PI/2f);
        MiddleManFloat = Mathf.RoundToInt(MiddleManFloat);
        MPHSpeed.text = MiddleManFloat.ToString();
    }
}
