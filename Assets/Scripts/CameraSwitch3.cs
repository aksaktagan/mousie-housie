using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch3 : MonoBehaviour
{
    public Camera camera3;

    public Camera camera1;
    public Camera camera2;

    bool camThreeIsActive;

    CameraSwitch1 cs1;
    CameraSwitch2 cs2;

    void Start()
    {
        cs1 = GameObject.FindFirstObjectByType<CameraSwitch1>();
        cs2 = GameObject.FindFirstObjectByType<CameraSwitch2>();
    }

    public void SwitchCamera()
    {
        // Disable the current camera
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        cs1.camOneSwitcher();
        cs2.camTwoSwitcher();

        // Enable the new current camera
        camera3.gameObject.SetActive(true);
        camThreeIsActive = true;
    }

    public void camThreeSwitcher()
    {
        camThreeIsActive = false;
    }

    public bool isCamThreeActive()
    {
        return camThreeIsActive;
    }
}
