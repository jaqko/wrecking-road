using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrashPointCalc : MonoBehaviour
{
    private string LastObjHit = "Nothing";
    public Rigidbody Car;
    public BoxCollider ThisPart;
    public static float ObjHit;
    public static float PartOfCar;
    public static float Speed;
    public int Points;
    public Text CountPoints;
    public LayerMask ObstacleMask;
    private bool Deactivated = false;
    public static int TotalPoints;
    void Start()
    {
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
            if (other.CompareTag("HouseBeam") && LastObjHit != "HouseBeam")
            {
                StopCoroutine(PointCooldown(0));
                LastObjHit = "House Beam";
                print("This Works");
                ObjHit = 25;
                Points = Mathf.RoundToInt(Speed) * Mathf.RoundToInt(PartOfCar) * Mathf.RoundToInt(ObjHit);
                TotalPoints = TotalPoints + Mathf.RoundToInt(Points);
                CountPoints.text = TotalPoints.ToString();
                print(Points);
                Deactivated = true;
            }

            if (other.CompareTag("Wall") && LastObjHit != "Wall")
            {
                StopCoroutine(PointCooldown(0));
                LastObjHit = "Wall";
                print("This Works");
                ObjHit = 30;
                Points = Mathf.RoundToInt(Speed) * Mathf.RoundToInt(PartOfCar) * Mathf.RoundToInt(ObjHit);
                TotalPoints = TotalPoints + Mathf.RoundToInt(Points);
                CountPoints.text = TotalPoints.ToString();
                print(Points);
                Deactivated = true;
            }

            if (other.CompareTag("Untagged"))
            {
                Deactivated = true;
                StopCoroutine(PointCooldown(0));
                print(Points);
                LastObjHit = "Untagged";
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
            TotalPoints = 0;
            CountPoints.text = TotalPoints.ToString();
        }
    }
    IEnumerator PointCooldown(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Deactivated = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (Deactivated == true && other)
        {
            Deactivated = false;
            print("True");
        }
    }
}
