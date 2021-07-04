using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public Canvas render;
    public Image render2;
    void Start()
    {
        SceneManager.LoadScene("SelectCar");
        render2.enabled = false;
        render.sortingOrder = -1;

    }

    // Update is called once per frame
    void Update()
    {
        if (SpedometerScript.FartOrNo == true)
        {
            render2.enabled = true;
            render.sortingOrder = 2;
            
        }
    }
}
