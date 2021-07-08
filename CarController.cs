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
    public float TopAccel;
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
            if (SpawnCar.CarName == "Caravan")
            {
                CarSpawned = "Caravan";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                CaravanSetup();
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Elantra")
            {
                CarSpawned = "Elantra";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                ElantraSetup();
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "Sentra")
            {
                CarSpawned = "Sentra";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                SentraSetup();
                LoadedIn = true;
                print("Success");
            }
            if (SpawnCar.CarName == "SL2")
            {
                CarSpawned = "SL2";
                CarBody = GameObject.Find(CarSpawned).GetComponent<Rigidbody>();
                CarSelect = GameObject.Find(CarSpawned);
                SL2Setup();
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
    }
    public void SL2Setup()
    {

        SL2.FLeft = GameObject.Find("WheelCollider SL2/Front Left").GetComponent<WheelCollider>(); SL2.FRight = GameObject.Find("WheelCollider SL2/Front Right").GetComponent<WheelCollider>(); SL2.RLeft = GameObject.Find("WheelCollider SL2/Rear Left").GetComponent<WheelCollider>(); SL2.RRight = GameObject.Find("WheelCollider SL2/Rear Right").GetComponent<WheelCollider>();
        SL2.FL = GameObject.Find("Wheels SL2/FrontLeft"); SL2.FR = GameObject.Find("Wheels SL2/FrontRight"); SL2.RL = GameObject.Find("Wheels SL2/RearLeftMain"); SL2.RR = GameObject.Find("Wheels SL2/RearRightMain");
        print("Setup SL2");
        CarName = SL2.ClassCarName;
        BTorque = SL2.BrakeTorque;
        Weight = SL2.Weight;
        Transmission = SL2.Transmission;
        maxGear = SL2.MaxGear;
        topSpeed = SL2.MaxCarSpeed;
        motorForce = SL2.MaxForce;
        FL = SL2.FL; FR = SL2.FR; RL = SL2.RL; RR = SL2.RR;
        FrontLeft = SL2.FLeft; FrontRight = SL2.FRight; RearLeft = SL2.RLeft; RearRight = SL2.RRight;
        GetBarrier = GameObject.Find("SL2/Barrier").GetComponent<BoxCollider>();
        thisCam.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
        GetBarrier.transform.parent = GameObject.Find("VehicleControl").GetComponent<Transform>();
    }

    public Car Rogue = new Car("Rogue",  5.96f, 3.16f, 3102, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 3531, 1, 'C');
    public Car GrandPrix = new Car("Grand Prix",6.24f, 3.16f, 3600, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 153, 3674, 1, 'A');
    public Car Caravan = new Car("Caravan", 7.96f, 3.16f, 4421, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 128, 4995, 1, 'A');
    public Car Elantra = new Car("Elantra", 4.41f, 3.16f, 2895, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 3270, 1, 'A');
    public Car Sentra = new Car("Sentra", 4.365f, 3.16f, 3124, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 118, 3531, 1, 'C');
    public Car SL2 = new Car("SL2", 3.79f, 3.16f, 2401, placeholderWheel, placeholderWheel, placeholderWheel, placeholderWheel, placeholder, placeholder, placeholder, placeholder, 106, 2714, 1, 'A');
}
