using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CinemachineCamera firstPersonVCam;
    public CinemachineCamera thirdPersonVCam;
    public void SwitchCameraView(InputAction.CallbackContext ctx)
    {
        
        if (firstPersonVCam != null && thirdPersonVCam != null)
        {
            if (firstPersonVCam.Priority > thirdPersonVCam.Priority)
            {
                firstPersonVCam.Priority = 10;
                thirdPersonVCam.Priority = 11;
            }
            else
            {
                firstPersonVCam.Priority = 11;
                thirdPersonVCam.Priority = 10;
            }
        }
    }
}
