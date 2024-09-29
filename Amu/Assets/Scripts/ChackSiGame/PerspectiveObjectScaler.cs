using UnityEngine;

public class PerspectiveObjectScaler : MonoBehaviour
{
    public Camera playerCamera;
    private Vector3 initialScale;
    private float initialDistance;

    void Start()
    {
        initialScale = transform.localScale;
        initialDistance = Vector3.Distance(transform.position, playerCamera.transform.position);
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, playerCamera.transform.position);
        float scaleFactor = currentDistance / initialDistance;
        transform.localScale = initialScale * scaleFactor;
    }
}
