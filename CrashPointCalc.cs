using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrashPointCalc : MonoBehaviour
{
    private string LastObjHit = "Inactive";
    public Rigidbody Car;
    public BoxCollider ThisPart;
    private float ObjHit;
    private float PartOfCar;
    public float Damage;
    private float Speed;
    private float Points;
    public Text CountPoints;
    private bool Deactivated = false;
    public static int TotalPoints;
    void Start()
    {
        Car = GameObject.Find(this.transform.parent.name).GetComponent<Rigidbody>();
        ThisPart = GetComponent<BoxCollider>();
        if (ThisPart.gameObject.name == "FrontBumper")
        {
            PartOfCar = 1.5f;
        } else if (ThisPart.gameObject.name == "RearBumper")
        {
            PartOfCar = 1.25f;
        } else if (ThisPart.gameObject.name == "Trunk")
        {
            PartOfCar = 1.75f;
        } else if (ThisPart.gameObject.name == "RightTailight" || ThisPart.gameObject.name == "LeftTailight")
        {
            PartOfCar = 1f;
        }
        Damage = 0;
    }

    RaycastHit hit;
    void Update()
    {
        Speed = Car.velocity.magnitude *(Mathf.PI / 2f) ;
        /*
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.1f, ObstacleMask) && Deactivated == false) {
            print("Ungh Daddy!");
            if (this.gameObject.name == "HouseBeam")
            {
                PartOfCar = 200;
            }

            Points = Speed * PartOfCar;
            CountPoints.text = Points.ToString();
        } else
        {
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Deactivated == false)
        {
            if (other.CompareTag("HouseBeam"))
            {
                LastObjHit = "HouseBeam";
                print("This Works");
                ObjHit = 25;
                Points = Speed * PartOfCar * ObjHit;
                TotalPoints = TotalPoints + Mathf.RoundToInt(Points);
                CountPoints.text = TotalPoints.ToString();
                print(Points);
            }

            if (other.CompareTag("WallPart"))
            {
                LastObjHit = "WallPart";
                print("This Works");
                ObjHit = 0.5f;
                Points = Speed * PartOfCar * ObjHit;
                TotalPoints = TotalPoints + Mathf.RoundToInt(Points);
                CountPoints.text = TotalPoints.ToString();
                print(Points);
            }
            
            if (other.CompareTag("Untagged") || other.CompareTag("Dead"))
            {
                print(Points);
                LastObjHit = "Inactive";
            }
            if (RoofScript.NoMore == 1) {
                TotalPoints = TotalPoints + 2500;
                CountPoints.text = TotalPoints.ToString();
                RoofScript.NoMore = 2;
            }
            if (other.tag == LastObjHit) { }
            // StartCoroutine(PointCooldown(2));
        }
        if (TotalPoints >= 5000)
        {
            SceneManager.UnloadSceneAsync("MainMap");
            SceneManager.LoadScene("TestMap");
            CountPoints.text = TotalPoints.ToString();
        }
    }
    IEnumerator PointCooldown(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Deactivated = false;
    }
}
