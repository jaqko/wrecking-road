using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnCar : MonoBehaviour
{
    public static string CarName;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Rogue()
    {
        CarName = "Rogue";
        print(CarName);
    }
    public void GrandPrix()
    {
        CarName = "Grand Prix";
        print(CarName);
    }
    public void Elantra()
    {
        CarName = "Elantra";
        print(CarName);
    }
    public void GrandCaravan()
    {
        CarName = "Caravan";
        print(CarName);
    }
    public void Sentra()
    {
        CarName = "Sentra";
        print(CarName);
    }
    public void Cruiser()
    {
        CarName = "Cruiser";
        print(CarName);
    }
    public void Forte()
    {
        CarName = "Forte";
        print(CarName);
    }
    public void Taurus()
    {
        CarName = "Taurus";
        print(CarName);
    }
    public void Durango()
    {
        CarName = "Durango";
        print(CarName);
    }
    public void Civic()
    {
        CarName = "Civic";
        print(CarName);
    }
    
    public void Ready()
    {
        SpedometerScript.FartOrNo = true;
        SceneManager.LoadScene("MainMap");
        SceneManager.UnloadSceneAsync("SelectCar");
    }
}
