using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float newHeight = 5f;
    public float newWidth = 10f;

    private Camera cameraComponent;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        ResizeCamera();
    }

    private void Update()
    {
        cameraComponent = GetComponent<Camera>();
        ResizeCamera();
    }

    private void ResizeCamera()
    {
        cameraComponent.orthographicSize = newHeight / 2f;
        cameraComponent.aspect = newWidth / newHeight;
    }
}
