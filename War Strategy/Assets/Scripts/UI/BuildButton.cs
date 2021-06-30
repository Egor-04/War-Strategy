using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public void BuildMode()
    {
        if (!CameraController.StaticCameraController.IsLock)
        {
            CameraController.StaticCameraController.IsLock = true;
        }
        else
        {
            CameraController.StaticCameraController.IsLock = false;
        }
    }
}
