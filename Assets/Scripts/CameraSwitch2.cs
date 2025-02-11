using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch2 : MonoBehaviour
{
    public Camera camera2;

    public Camera camera1;
    public Camera camera3;

    bool camTwoIsActive;

    CameraSwitch1 cs1;
    CameraSwitch3 cs3;

    void Start()
    {
        cs1 = GameObject.FindFirstObjectByType<CameraSwitch1>();
        cs3 = GameObject.FindFirstObjectByType<CameraSwitch3>();
    }

    public void SwitchCamera()
    {
        // Disable the current camera
        camera1.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        cs1.camOneSwitcher();
        cs3.camThreeSwitcher();

        // Enable the new current camera
        camera2.gameObject.SetActive(true);
        camTwoIsActive = true;
    }

    public void camTwoSwitcher()
    {
        camTwoIsActive = false;
    }

    public bool isCamTwoActive()
    {
        return camTwoIsActive;
    }
}
