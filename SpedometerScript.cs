using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpedometerScript : MonoBehaviour
{
    public Rigidbody grabCarSpeed;
    Text MPHSpeed;
    public static bool FartOrNo = false;
    void Start()
    {

        MPHSpeed = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CarController.CarName == "Rogue")
        {
            grabCarSpeed = GameObject.Find("Rogue").GetComponent<Rigidbody>();
        }
        else if (CarController.CarName == "Grand Prix")
        {
            grabCarSpeed = GameObject.Find("Grand Prix").GetComponent<Rigidbody>();
        }
        else if (CarController.CarName == "Caravan")
        {
            grabCarSpeed = GameObject.Find("Caravan").GetComponent<Rigidbody>();
        }
        else if (CarController.CarName == "Elantra")
        {
            grabCarSpeed = GameObject.Find("Elantra").GetComponent<Rigidbody>();
        }
        else if (CarController.CarName == "Sentra")
        {
            grabCarSpeed = GameObject.Find("Sentra").GetComponent<Rigidbody>();
        }
        else if (CarController.CarName == "Cruiser")
        {
            grabCarSpeed = GameObject.Find("Cruiser").GetComponent<Rigidbody>();
        }
        float MiddleManFloat = grabCarSpeed.velocity.magnitude * (Mathf.PI / 2);
        MiddleManFloat = Mathf.RoundToInt(MiddleManFloat);
        MPHSpeed.text = MiddleManFloat.ToString();
    }
}
