using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public static Camera thisCam;
    public static CarController Vehicle;
    public static float VehicleMultiplier;
    public float TopAccel;
    public static GameObject VehicleGO;
    public BoxCollider GetBarrier;
    private bool LoadedIn = false;
    public Rigidbody CarBody;
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
    public float RotationInWheels = 45;
    public float Acceleration;
    public char Transmission;
    public float Weight;
    public int gear;
    public Text GearText;
    public class Car
    {
        public string ClassCarName;
        public float BrakeTorque;
        public float TopAccel;
        public float Weight;
        public GameObject placeholder;
        public WheelCollider placeholderWheel;
        public GameObject FL, FR, RL, RR;
        public WheelCollider FLeft, FRight, RLeft, RRight;
        public float MaxCarSpeed;
        public float MaxForce;
        public int MaxGear;
        public char Transmission;

        public Car(string cname, float bt, float topacc, float cmass, WheelCollider FLWc, WheelCollider FRWc, WheelCollider RLWc, WheelCollider RRWc, GameObject wheelfl, GameObject wheelfr, GameObject wheelrl, GameObject wheelrr, float carmaxspd, float carmaxfrc, int carmaxg, char trans)
        {
            ClassCarName = cname;
            BrakeTorque = bt;
            TopAccel = topacc;
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

    public Car SpawnedVehicle = new Car("None",/* basic */ 3, 1, 3102, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 125, 4102, 1, 'C');
    /*public Car GP = new Car("Grand Prix", 0.25f *//* basic *//* , 3102, placeholder, placeholder, placeholder, placeholder, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, 125, 1, 'C', 0);*/
    private static GameObject placeholder;
    private static WheelCollider placeholderWheel;

    void Awake()
    {
        
    }
    void Start()
    {
        Spawn();
        VehicleGO = GameObject.Find("VehicleControl");
        
    }
    void Spawn()
    {
        if (LoadedIn == false)
        {
            if (SpawnCar.CarName == "Rogue")
            {
                CarSpawned = "Rogue";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                RogueSetup();
                VehicleMultiplier = 2.25f;
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Grand Prix")
            {
                CarSpawned = "Grand Prix";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                GPSetup();
                VehicleMultiplier = 2;
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Caravan")
            {
                CarSpawned = "Caravan";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                CaravanSetup();
                VehicleMultiplier = 1.75f;
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Elantra")
            {
                CarSpawned = "Elantra";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                ElantraSetup();
                VehicleMultiplier = 0.75f;
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Sentra")
            {
                CarSpawned = "Sentra";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                SentraSetup();
                VehicleMultiplier = 1;
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Cruiser")
            {
                CarSpawned = "Cruiser";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                CruiserSetup();
                VehicleMultiplier = 0.5f;
                LoadedIn = true;
                print("Success");
            }
        }
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
        /*Accelerate();*/
        Steering();
        TurnAngle();
        GearCheck();
    }
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
        }
        if (gear == -1)
        {
            ver = -Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.S))
            {
                ver = 0;
                FrontLeft.brakeTorque = BTorque * ver;
                FrontRight.brakeTorque = BTorque * ver;
                FrontLeft.motorTorque = motorForce * ver;
                FrontRight.motorTorque = motorForce * ver;
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
        if (gear == 0)
        {
            FrontLeft.motorTorque = 0 * ver;
            FrontRight.motorTorque = 0 * ver;
        }
        if (ver < 0)
        {
            print(ver);
            FrontLeft.brakeTorque = BTorque * ver;
            FrontRight.brakeTorque = BTorque * ver;
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                ver = 0;
                FrontLeft.brakeTorque = BTorque * ver;
                FrontRight.brakeTorque = BTorque * ver;
                FrontLeft.motorTorque = motorForce * ver;
                FrontRight.motorTorque = motorForce * ver;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                print(ver);
                FrontLeft.brakeTorque = 0 * ver;
                FrontRight.brakeTorque = 0 * ver;
                FrontLeft.motorTorque = motorForce * ver;
                FrontRight.motorTorque = motorForce * ver;
                FrontLeft.motorTorque = FrontLeft.motorTorque;
                FrontRight.motorTorque = FrontRight.motorTorque;
            } else
            {
                FrontLeft.brakeTorque = 0 * ver;
                FrontRight.brakeTorque = 0 * ver;
                FrontLeft.motorTorque = 0 * ver;
                FrontRight.motorTorque = 0 * ver;
            }
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
        Rogue.FLeft = GameObject.Find("WheelCollider Rogue/Front Left").GetComponent<WheelCollider>(); Rogue.FRight = GameObject.Find("WheelCollider Rogue/Front Right").GetComponent<WheelCollider>(); Rogue.RLeft = GameObject.Find("WheelCollider Rogue/Rear Left").GetComponent<WheelCollider>(); Rogue.RRight = GameObject.Find("WheelCollider Rogue/Rear Right").GetComponent<WheelCollider>();
        print("Setup Rogue");
        CarName = Rogue.ClassCarName;
        BTorque = Rogue.BrakeTorque;
        TopAccel = Rogue.TopAccel;
        Weight = Rogue.Weight;
        Transmission = Rogue.Transmission;
        maxGear = Rogue.MaxGear;
        topSpeed = Rogue.MaxCarSpeed;
        motorForce = Rogue.MaxForce;
        FL = Rogue.FL; FR = Rogue.FR; RL = Rogue.RL; RR = Rogue.RR;
        FrontLeft = Rogue.FLeft; FrontRight = Rogue.FRight; RearLeft = Rogue.RLeft; RearRight = Rogue.RRight;
        GetBarrier = GameObject.Find("Rogue/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    public void GPSetup()
    {
        
        GrandPrix.FLeft = GameObject.Find("WheelCollider GP/Front Left").GetComponent<WheelCollider>(); GrandPrix.FRight = GameObject.Find("WheelCollider GP/Front Right").GetComponent<WheelCollider>(); GrandPrix.RLeft = GameObject.Find("WheelCollider GP/Rear Left").GetComponent<WheelCollider>(); GrandPrix.RRight = GameObject.Find("WheelCollider GP/Rear Right").GetComponent<WheelCollider>();
        GrandPrix.FL = GameObject.Find("Wheels GP/FrontLeft"); GrandPrix.FR = GameObject.Find("Wheels GP/FrontRight"); GrandPrix.RL = GameObject.Find("Wheels GP/RearLeftMain"); GrandPrix.RR = GameObject.Find("Wheels GP/RearRightMain");
        print("Setup GP");
        CarName = GrandPrix.ClassCarName;
        BTorque = GrandPrix.BrakeTorque;
        TopAccel = GrandPrix.TopAccel;
        Weight = GrandPrix.Weight;
        Transmission = GrandPrix.Transmission;
        maxGear = GrandPrix.MaxGear;
        topSpeed = GrandPrix.MaxCarSpeed;
        motorForce = GrandPrix.MaxForce;
        FL = GrandPrix.FL; FR = GrandPrix.FR; RL = GrandPrix.RL; RR = GrandPrix.RR;
        FrontLeft = GrandPrix.FLeft; FrontRight = GrandPrix.FRight; RearLeft = GrandPrix.RLeft; RearRight = GrandPrix.RRight;
        GetBarrier = GameObject.Find("Grand Prix/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    public void CaravanSetup()
    {
        Caravan.FLeft = GameObject.Find("WheelCollider Caravan/Front Left").GetComponent<WheelCollider>(); Caravan.FRight = GameObject.Find("WheelCollider Caravan/Front Right").GetComponent<WheelCollider>(); Caravan.RLeft = GameObject.Find("WheelCollider Caravan/Rear Left").GetComponent<WheelCollider>(); Caravan.RRight = GameObject.Find("WheelCollider Caravan/Rear Right").GetComponent<WheelCollider>();
        Caravan.FL = GameObject.Find("Wheels Caravan/FrontLeft"); Caravan.FR = GameObject.Find("Wheels Caravan/FrontRight"); Caravan.RL = GameObject.Find("Wheels Caravan/RearLeftMain"); Caravan.RR = GameObject.Find("Wheels Caravan/RearRightMain");
        print("Setup Caravan");
        CarName = Caravan.ClassCarName;
        BTorque = Caravan.BrakeTorque;
        TopAccel = Caravan.TopAccel;
        Weight = Caravan.Weight;
        Transmission = Caravan.Transmission;
        maxGear = Caravan.MaxGear;
        topSpeed = Caravan.MaxCarSpeed;
        motorForce = Caravan.MaxForce;
        FL = Caravan.FL; FR = Caravan.FR; RL = Caravan.RL; RR = Caravan.RR;
        FrontLeft = Caravan.FLeft; FrontRight = Caravan.FRight; RearLeft = Caravan.RLeft; RearRight = Caravan.RRight;
        GetBarrier = GameObject.Find("Caravan/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    public void ElantraSetup()
    {
        Elantra.FLeft = GameObject.Find("WheelCollider Elantra/Front Left").GetComponent<WheelCollider>(); Elantra.FRight = GameObject.Find("WheelCollider Elantra/Front Right").GetComponent<WheelCollider>(); Elantra.RLeft = GameObject.Find("WheelCollider Elantra/Rear Left").GetComponent<WheelCollider>(); Elantra.RRight = GameObject.Find("WheelCollider Elantra/Rear Right").GetComponent<WheelCollider>();
        Elantra.FL = GameObject.Find("Wheels Elantra/FrontLeft"); Elantra.FR = GameObject.Find("Wheels Elantra/FrontRight"); Elantra.RL = GameObject.Find("Wheels Elantra/RearLeftMain"); Elantra.RR = GameObject.Find("Wheels Elantra/RearRightMain");
        print("Setup Elantra");
        CarName = Elantra.ClassCarName;
        BTorque = Elantra.BrakeTorque;
        TopAccel = Elantra.TopAccel;
        Weight = Elantra.Weight;
        Transmission = Elantra.Transmission;
        maxGear = Elantra.MaxGear;
        topSpeed = Elantra.MaxCarSpeed;
        motorForce = Elantra.MaxForce;
        FL = Elantra.FL; FR = Elantra.FR; RL = Elantra.RL; RR = Elantra.RR;
        FrontLeft = Elantra.FLeft; FrontRight = Elantra.FRight; RearLeft = Elantra.RLeft; RearRight = Elantra.RRight;
        GetBarrier = GameObject.Find("Elantra/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    public void SentraSetup()
    {
        Sentra.FLeft = GameObject.Find("WheelCollider Sentra/Front Left").GetComponent<WheelCollider>(); Sentra.FRight = GameObject.Find("WheelCollider Sentra/Front Right").GetComponent<WheelCollider>(); Sentra.RLeft = GameObject.Find("WheelCollider Sentra/Rear Left").GetComponent<WheelCollider>(); Sentra.RRight = GameObject.Find("WheelCollider Sentra/Rear Right").GetComponent<WheelCollider>();
        Sentra.FL = GameObject.Find("Wheels Sentra/FrontLeft"); Sentra.FR = GameObject.Find("Wheels Sentra/FrontRight"); Sentra.RL = GameObject.Find("Wheels Sentra/RearLeftMain"); Sentra.RR = GameObject.Find("Wheels Sentra/RearRightMain");
        print("Setup Sentra");
        CarName = Sentra.ClassCarName;
        BTorque = Sentra.BrakeTorque;
        TopAccel = Sentra.TopAccel;
        Weight = Sentra.Weight;
        Transmission = Sentra.Transmission;
        maxGear = Sentra.MaxGear;
        topSpeed = Sentra.MaxCarSpeed;
        motorForce = Sentra.MaxForce;
        FL = Sentra.FL; FR = Sentra.FR; RL = Sentra.RL; RR = Sentra.RR;
        FrontLeft = Sentra.FLeft; FrontRight = Sentra.FRight; RearLeft = Sentra.RLeft; RearRight = Sentra.RRight;
        GetBarrier = GameObject.Find("Sentra/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    public void CruiserSetup()
    {
        Cruiser.FLeft = GameObject.Find("WheelCollider Cruiser/Front Left").GetComponent<WheelCollider>(); Cruiser.FRight = GameObject.Find("WheelCollider Cruiser/Front Right").GetComponent<WheelCollider>(); Cruiser.RLeft = GameObject.Find("WheelCollider Cruiser/Rear Left").GetComponent<WheelCollider>(); Cruiser.RRight = GameObject.Find("WheelCollider Cruiser/Rear Right").GetComponent<WheelCollider>();
        Cruiser.FL = GameObject.Find("Wheels Cruiser/FrontLeft"); Cruiser.FR = GameObject.Find("Wheels Cruiser/FrontRight"); Cruiser.RL = GameObject.Find("Wheels Cruiser/RearLeftMain"); Cruiser.RR = GameObject.Find("Wheels Cruiser/RearRightMain");
        print("Setup Cruiser");
        CarName = Cruiser.ClassCarName;
        BTorque = Cruiser.BrakeTorque;
        Weight = Cruiser.Weight;
        Transmission = Cruiser.Transmission;
        maxGear = Cruiser.MaxGear;
        topSpeed = Cruiser.MaxCarSpeed;
        motorForce = Cruiser.MaxForce;
        FL = Cruiser.FL; FR = Cruiser.FR; RL = Cruiser.RL; RR = Cruiser.RR;
        FrontLeft = Cruiser.FLeft; FrontRight = Cruiser.FRight; RearLeft = Cruiser.RLeft; RearRight = Cruiser.RRight;
        GetBarrier = GameObject.Find("Cruiser/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        CarBody.transform.position = new Vector3(-58.75f, 2.65f, -13.11008f);
    }
    // multiply motor force by two
    // vvv     vvv    vvv     vvv
    public Car Rogue = new Car("Rogue", 11.92f, 3.16f, 3102, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 7062, 1, 'C');
    public Car GrandPrix = new Car("Grand Prix", 12.48f, 3.16f, 3600, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 153, 7348, 1, 'A');
    public Car Caravan = new Car("Caravan", 15.92f, 3.16f, 4421, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 128, 9990, 1, 'A');
    public Car Elantra = new Car("Elantra", 8.82f, 3.16f, 2895, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 6540, 1, 'A');
    public Car Sentra = new Car("Sentra", 8.73f, 3.16f, 3124, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 7062, 1, 'C');
    /* in progress */ public Car Cruiser = new Car("Cruiser", 7.58f, 3.16f, 3108, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 106, 5428, 1, 'A');
}
