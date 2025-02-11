using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch1 : MonoBehaviour
{
    public Camera camera1;

    public Camera camera2;
    public Camera camera3;

    bool camOneIsActive = true;

    CameraSwitch2 cs2;
    CameraSwitch3 cs3;

    void Start()
    {
        cs2 = GameObject.FindFirstObjectByType<CameraSwitch2>();
        cs3 = GameObject.FindFirstObjectByType<CameraSwitch3>();
    }

    public void SwitchCamera()
    {
        // Disable the current camera
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        cs2.camTwoSwitcher();
        cs3.camThreeSwitcher();

        // Enable the new current camera
        camera1.gameObject.SetActive(true);
        camOneIsActive = true;
    }

    public void camOneSwitcher()
    {
        camOneIsActive = false;
    }

    public bool isCamOneActive()
    {
        return camOneIsActive;
    }

}
