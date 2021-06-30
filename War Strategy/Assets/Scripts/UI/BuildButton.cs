using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public void BuildMode()
    {
        if (!CameraController.StaticCameraController.IsLock)
        {
            CameraController.StaticCameraController.IsLock = false;
        }
        else
        {
            CameraController.StaticCameraController.IsLock = true;
        }
    }
}
