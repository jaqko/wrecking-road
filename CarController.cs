using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Camera currentCam;
    public static CarController Vehicle;
    public static GameObject VehicleGO;
    public BoxCollider GetBarrier;
    private bool LoadedIn = false;
    public Rigidbody CarBody;
    private GameObject FindCurrentWheel;
    public float BTorque;
    public GameObject CarSelect;
    public float topSpeed;
    [Tooltip("Wheels")] public GameObject FL;[Tooltip("Wheels")] public GameObject FR;[Tooltip("Wheels")] public GameObject RL;[Tooltip("Wheels")] public GameObject RR;
    public WheelCollider FrontLeft; public WheelCollider FrontRight; public WheelCollider RearLeft; public WheelCollider RearRight;
    public GameObject findOurWheelPos;
    public int maxGear;
    public float maxGearSpeed;
    public static string CarName;
    public float motorForce;
    public float maxSteer;
    public string CarSpawned = "None";
    private float hor;
    private float ver;
    private GameObject FindCurrentScript;
    private float WheelAngle;
    public float RotationInWheels = 45;
    public float Acceleration;
    public char Transmission;
    public float Weight;
    public int gear;
    public Text GearText;
    private Vector3 SpawnPos = new Vector3(1.381528f, 16.15f, -77.29884f);
    public class Car
    {
        public string ClassCarName;
        public float BrakeTorque;
        public float Weight;
        public GameObject placeholder;
        public WheelCollider placeholderWheel;
        public GameObject FL, FR, RL, RR;
        public WheelCollider FLeft, FRight, RLeft, RRight;
        public float MaxCarSpeed;
        public float MaxForce;
        public int MaxGear;
        public char Transmission;

        public Car(string cname, float bt, float cmass, WheelCollider FLWc, WheelCollider FRWc, WheelCollider RLWc, WheelCollider RRWc, GameObject wheelfl, GameObject wheelfr, GameObject wheelrl, GameObject wheelrr, float carmaxspd, float carmaxfrc, int carmaxg, char trans)
        {
            ClassCarName = cname;
            BrakeTorque = bt;
            Weight = cmass;
            FL = wheelfl;
            FR = wheelfr;
            RL = wheelrl;
            RR = wheelrr;
            FLeft = FLWc; FRight = FRWc; RLeft = RLWc; RRight = RRWc;
            MaxCarSpeed = carmaxspd;
            MaxForce = carmaxfrc;
            MaxGear = carmaxg;
            Transmission = trans;
        }
    }

    public Car SpawnedVehicle = new Car("None", /* basic */ 3, 3102, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 125, 4102, 1, 'C');
    /*public Car GP = new Car("Grand Prix", 0.25f *//* basic *//* , 3102, placeholder, placeholder, placeholder, placeholder, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, 125, 1, 'C', 0);*/
    private static GameObject placeholder;
    private static WheelCollider placeholderWheel;

    void Awake()
    {

        
    }
    void Start()
    {
        if (LoadedIn == false)
        {
            if (SpawnCar.CarName == "Rogue")
            {
                CarSpawned = "Rogue";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                RogueSetup();
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Grand Prix")
            {
                CarSpawned = "Grand Prix";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                GPSetup();
                LoadedIn = true;
                print("Success");
            }

        }
        VehicleGO = GameObject.Find("VehicleControl");
    }
    /* List for car transmission:
    -2 = Park
    -1 = Reverse
    0 = Neutral
    1 = 1st Gear (For automatic / CVT transmissions, Drive)
    2 = 2nd Gear (For automatic / CVT transmissions, Super)
    3 = 3rd Gear
    4 = 4th Gear
    5 = 5th Gear
    6 = 6th Gear
    7 = 7th Gear
    8 = 8th Gear
    9 = 9th Gear
    10 = 10th Gear
    */

    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Accelerate();
        Steering();
        TurnAngle();
        GearCheck();
    }
/*    void SpawnInCars()
    {
        if (CarName == "Rogue" && CarSpawned != "Rogue")
        {
            CarSpawned = "Rogue";
            CarSelect.transform.position = new Vector3(0.93f, 2.52f, 24.26f);
            CarSelect = GameObject.Find(CarName);
            CarBody = CarSelect.GetComponent<Rigidbody>();
            print(CarBody.name);
            CarSelect.transform.position = SpawnPos;
            CarSelect.transform.rotation = Quaternion.identity;
            StartCoroutine(WaitForCarConstraints(0f));
        }
        if (CarName == "Grand Prix" && CarSpawned != "Grand Prix")
        {
            CarSpawned = "Grand Prix";
            CarSelect.transform.position = new Vector3(12.93f, 2.52f, 24.26f);
            CarSelect = GameObject.Find(CarName);
            CarBody = CarSelect.GetComponent<Rigidbody>();
            print(CarBody.name);
            GPSetup();
            CarSelect.transform.position = SpawnPos;
            CarSelect.transform.rotation = Quaternion.identity;
            StartCoroutine(WaitForCarConstraints(0f));
        }
    }*/
    void GearCheck()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gear != -2)
            {
                gear--;
            } else 
            {
                gear = -2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (gear != maxGear)
            {
                gear++;
            }
            else
            {
                gear = maxGear;
            }
        }
        if (gear == -2)
        {
            GearText.text = "P";
        }
        else if (gear == 0)
        {
            GearText.text = "N";
        }
        else if (gear == -1)
        {
            GearText.text = "R";
        }
        else if (gear >= 1 && Transmission == 'C' || gear >= 1 && Transmission == 'A')
        {
            GearText.text = "D";
        }
        else if (gear >= 1 && Transmission == 'M')
        {
            GearText.text = gear.ToString();
        }
    }
    void Accelerate()
    {
        if (gear == -2)
        {
            FrontLeft.brakeTorque = BTorque * 1;
            FrontRight.brakeTorque = BTorque * 1;
            FrontLeft.motorTorque = 0 * ver;
            FrontRight.motorTorque = 0 * ver;
            return;
        }
        if (gear == -1)
        {
            ver = -Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.S))
            {
                print(ver);
                FrontLeft.brakeTorque = BTorque * ver;
                FrontRight.brakeTorque = BTorque * ver;
            }
            else
            {
                print(ver);
                FrontLeft.brakeTorque = 0 * ver;
                FrontRight.brakeTorque = 0 * ver;
                FrontLeft.motorTorque = motorForce * ver;
                FrontRight.motorTorque = motorForce * ver;
            }
            return;
        }
        if (gear == 0)
        {
            FrontLeft.motorTorque = 0 * ver;
            FrontRight.motorTorque = 0 * ver;
            return;
        }
        if (ver < 0)
        {
            print(ver);
            FrontLeft.brakeTorque = BTorque * ver;
            FrontRight.brakeTorque = BTorque * ver;
        }
        else
        {
            print(ver);
            FrontLeft.brakeTorque = 0 * ver;
            FrontRight.brakeTorque = 0 * ver;
            FrontLeft.motorTorque = motorForce * ver;
            FrontRight.motorTorque = motorForce * ver;
        }
    }
    void Steering()
    {
        FrontLeft.steerAngle = maxSteer * hor;
        FrontRight.steerAngle = maxSteer * hor;
    }
    private void TurnAngle()
    {
        UpdateWheelPose(FrontRight, FR.transform);
        UpdateWheelPose(FrontLeft, FL.transform);
        UpdateWheelPose(RearRight, RR.transform);
        UpdateWheelPose(RearLeft, RL.transform);
    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    public void RogueSetup()
    {
        Rogue.FL = GameObject.Find("Wheels Rogue/FrontLeft"); Rogue.FR = GameObject.Find("Wheels Rogue/FrontRight"); Rogue.RL = GameObject.Find("Wheels Rogue/RearLeftMain"); Rogue.RR = GameObject.Find("Wheels Rogue/RearRightMain");
        print("Setup Rogue");
        CarName = Rogue.ClassCarName;
        BTorque = Rogue.BrakeTorque;
        Weight = Rogue.Weight;
        Transmission = Rogue.Transmission;
        maxGear = Rogue.MaxGear;
        topSpeed = Rogue.MaxCarSpeed;
        motorForce = Rogue.MaxForce;
        FL = Rogue.FL; FR = Rogue.FR; RL = Rogue.RL; RR = Rogue.RR;
        FL.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        FR.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        RL.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        RR.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        print(Rogue.MaxForce);
    }
    public void GPSetup()
    {
        
        GrandPrix.FLeft = GameObject.Find("WheelCollider GP/Front Left").GetComponent<WheelCollider>(); GrandPrix.FRight = GameObject.Find("WheelCollider GP/Front Right").GetComponent<WheelCollider>(); GrandPrix.RLeft = GameObject.Find("WheelCollider GP/Rear Left").GetComponent<WheelCollider>(); GrandPrix.RRight = GameObject.Find("WheelCollider GP/Rear Right").GetComponent<WheelCollider>();
        GrandPrix.FL = GameObject.Find("Wheels GP/FrontLeft"); GrandPrix.FR = GameObject.Find("Wheels GP/FrontRight"); GrandPrix.RL = GameObject.Find("Wheels GP/RearLeftMain"); GrandPrix.RR = GameObject.Find("Wheels GP/RearRightMain");
        print("Setup GP");
        CarName = GrandPrix.ClassCarName;
        BTorque = GrandPrix.BrakeTorque;
        Weight = GrandPrix.Weight;
        Transmission = GrandPrix.Transmission;
        maxGear = GrandPrix.MaxGear;
        topSpeed = GrandPrix.MaxCarSpeed;
        motorForce = GrandPrix.MaxForce;
        FL = GrandPrix.FL; FR = GrandPrix.FR; RL = GrandPrix.RL; RR = GrandPrix.RR;
        FrontLeft = GrandPrix.FLeft; FrontRight = GrandPrix.FRight; RearLeft = GrandPrix.RLeft; RearRight = GrandPrix.RRight;
        FL.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        FR.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        RL.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        RR.transform.parent = GameObject.Find("Wheels").GetComponent<Transform>();
        FrontLeft.transform.parent = GameObject.Find("WheelCollider").GetComponent<Transform>();
        FrontRight.transform.parent = GameObject.Find("WheelCollider").GetComponent<Transform>();
        RearLeft.transform.parent = GameObject.Find("WheelCollider").GetComponent<Transform>();
        RearRight.transform.parent = GameObject.Find("WheelCollider").GetComponent<Transform>();
        currentCam = GameObject.Find("Grand Prix/GPCam").GetComponent<Camera>();
        GetBarrier = GameObject.Find("Grand Prix/Barrier").GetComponent<BoxCollider>();
        currentCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
    }


    public Car Rogue = new Car("Rogue",  5.96f, 3102, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 3531, 1, 'C');
    public Car GrandPrix = new Car("Grand Prix",  6.24f, 3600, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 153, 3674, 1, 'A');
}
