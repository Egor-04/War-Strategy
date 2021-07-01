using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public void BuildMode(GameObject buildingPanel)
    {
        if (!CameraController.StaticCameraController.IsLock)
        {
            if (!buildingPanel.activeSelf)
            {
                buildingPanel.SetActive(true);
            }

            CameraController.StaticCameraController.IsLock = true;
        }
        else
        {
            if (buildingPanel.activeSelf)
            {
                buildingPanel.SetActive(false);
            }

            CameraController.StaticCameraController.IsLock = false;
        }
    }
}
