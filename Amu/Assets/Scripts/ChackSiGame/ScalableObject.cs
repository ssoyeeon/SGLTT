using UnityEngine;

public class ScalableObject : MonoBehaviour
{
    private Camera playerCamera;
    private Vector3 initialScale;
    public float scaleRate = 1f; // 스케일 변화 비율
    public float minScaleMultiplier = 0.25f;
    public float maxScaleMultiplier = 3f;

    void Start()
    {
        playerCamera = Camera.main;
        initialScale = transform.localScale;
    }

    void Update()
    {
        AdjustScaleBasedOnDistance();
    }

    private void AdjustScaleBasedOnDistance()
    {
        float distance = Vector3.Distance(transform.position, playerCamera.transform.position);
        float targetScaleFactor = Mathf.Clamp(distance * scaleRate, minScaleMultiplier, maxScaleMultiplier);
        Vector3 targetScale = initialScale * targetScaleFactor;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleRate);
    }
}
