using UnityEngine;

public class ForcedPerspective : MonoBehaviour
{
    public Camera mainCamera;      // 메인 카메라 참조
    public float grabDistance = 100f;  // 물체를 잡을 수 있는 최대 거리
    public float placeDistance = 10f;  // 물체를 놓을 기본 거리
    public float zoomSpeed = 1f;       // 줌 속도 (마우스 휠 감도)
    public float minDistance = 1f;     // 물체를 가져올 수 있는 최소 거리
    public float maxDistance = 20f;    // 물체를 보낼 수 있는 최대 거리

    private GameObject grabbedObject;  // 현재 잡고 있는 물체
    private Vector3 originalScale;     // 물체의 원래 크기
    private float originalDistance;    // 물체를 잡았을 때의 원래 거리

    void Update()
    {
        // 왼쪽 마우스 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedObject == null)
            {
                GrabObject();  // 물체를 잡음
            }
            else
            {
                PlaceObject(); // 물체를 놓음
            }
        }

        // 물체를 잡고 있는 경우
        if (grabbedObject != null)
        {
            UpdateObjectPosition();  // 물체 위치 업데이트
            AdjustObjectDistance();  // 물체 거리 조절
        }
    }

    void GrabObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // 레이캐스트로 물체 감지
        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            grabbedObject = hit.collider.gameObject;
            originalScale = grabbedObject.transform.localScale;
            originalDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
            placeDistance = Mathf.Min(hit.distance, placeDistance); // 초기 거리 설정
        }
    }

    void UpdateObjectPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newPosition = ray.GetPoint(placeDistance);
        grabbedObject.transform.position = newPosition;

        // 거리에 따른 스케일 조정
        float currentDistance = Vector3.Distance(mainCamera.transform.position, newPosition);
        float scaleFactor = currentDistance / originalDistance;
        grabbedObject.transform.localScale = originalScale * scaleFactor;
    }

    void AdjustObjectDistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            // 마우스 휠 입력에 따라 거리 조절
            placeDistance -= scrollInput * zoomSpeed;
            // 거리를 최소값과 최대값 사이로 제한
            placeDistance = Mathf.Clamp(placeDistance, minDistance, maxDistance);
        }
    }

    void PlaceObject()
    {
        // 물체를 놓음 (잡고 있는 물체 참조 제거)
        grabbedObject = null;
    }
}