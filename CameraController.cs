using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera RogueCam;
    public Camera GPCam;
    public Camera ElantraCam;
    public Camera CaravanCam;
    public Camera SentraCam;
    public Camera CruiserCam;
    public Camera ForteCam;
    public Camera TaurusCam;
    public Camera DurangoCam;
    public Camera CivicCam;
    void Start()
    {
        RogueCam.enabled = false;
        GPCam.enabled = false;
        ElantraCam.enabled = false;
        CaravanCam.enabled = false;
        SentraCam.enabled = false;
        CruiserCam.enabled = false;
        ForteCam.enabled = false;
        TaurusCam.enabled = false;
        DurangoCam.enabled = false;
        CivicCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CarController.CarName == "Rogue")
        {
            RogueCam.enabled = true;
            GPCam.enabled = false;
            ElantraCam.enabled = false;
            CaravanCam.enabled = false;
            SentraCam.enabled = false;
            CruiserCam.enabled = false;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = RogueCam;
        } else if (CarController.CarName == "Grand Prix")
        {
            RogueCam.enabled = false;
            GPCam.enabled = true;
            ElantraCam.enabled = false;
            CaravanCam.enabled = false;
            SentraCam.enabled = false;
            CruiserCam.enabled = false;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = GPCam;
        }
        else if (CarController.CarName == "Caravan")
        {
            RogueCam.enabled = false;
            GPCam.enabled = false;
            ElantraCam.enabled = false;
            CaravanCam.enabled = true;
            SentraCam.enabled = false;
            CruiserCam.enabled = false;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = CaravanCam;
        }
        else if (CarController.CarName == "Elantra")
        {
            RogueCam.enabled = false;
            GPCam.enabled = false;
            ElantraCam.enabled = true;
            CaravanCam.enabled = false;
            SentraCam.enabled = false;
            CruiserCam.enabled = false;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = ElantraCam;
        }
        else if (CarController.CarName == "Sentra")
        {
            RogueCam.enabled = false;
            GPCam.enabled = false;
            ElantraCam.enabled = false;
            CaravanCam.enabled = false;
            SentraCam.enabled = true;
            CruiserCam.enabled = false;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = SentraCam;
        }
        else if (CarController.CarName == "Cruiser")
        {
            RogueCam.enabled = false;
            GPCam.enabled = false;
            ElantraCam.enabled = false;
            CaravanCam.enabled = false;
            SentraCam.enabled = false;
            CruiserCam.enabled = true;
            ForteCam.enabled = false;
            TaurusCam.enabled = false;
            DurangoCam.enabled = false;
            CivicCam.enabled = false;
            CarController.thisCam = CruiserCam;
        }
    }
}
