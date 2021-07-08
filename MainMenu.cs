using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image Imageboody;
    public Button Buttonboody;
    public Image Buttonbooody; public Text Buttonboooody;
    void Start()
    {
        Imageboody.enabled = false;
        Buttonboody.enabled = false;
        Buttonbooody.enabled = false; Buttonboooody.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("SelectCar");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
    public void Mods()
    {
    }
    public void Console()
    {
        Imageboody.enabled = true;
        Buttonboody.enabled = true;
        Buttonbooody.enabled = true; Buttonboooody.enabled = true;
    }
    public void BackfromConsole()
    {
        Imageboody.enabled = false;
        Buttonboody.enabled = false;
        Buttonbooody.enabled = false; Buttonboooody.enabled = false;
    }

}
