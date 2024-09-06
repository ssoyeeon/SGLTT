using UnityEngine;

public class ForcedPerspective : MonoBehaviour
{
    // 공개 변수들: Unity Inspector에서 설정 가능
    public Camera mainCamera;  // 메인 카메라 참조
    public float grabDistance = 100f;  // 물체를 잡을 수 있는 최대 거리
    public float placeDistance = 10f;  // 물체를 놓을 기본 거리
    public float zoomSpeed = 1f;  // 줌 속도 (스크롤 민감도)
    public float minDistance = 1f;  // 물체를 카메라에 가져올 수 있는 최소 거리
    public float maxDistance = 20f;  // 물체를 카메라에서 멀리 둘 수 있는 최대 거리
    public float rotationSpeed = 20f;  // 물체 회전 속도
    public float moveSpeed = 10f;  // 물체 이동 속도
    public LayerMask wallLayer;  // 벽으로 간주할 레이어
    public Color rayColor = Color.red;  // 디버그 레이의 색상
    public float rayDuration = 0.1f;  // 디버그 레이가 보이는 시간

    // 비공개 변수들: 내부 로직에 사용
    private GameObject grabbedObject;  // 현재 잡고 있는 물체
    private Vector3 originalScale;  // 물체의 원래 크기
    private float originalDistance;  // 물체를 잡았을 때의 원래 거리
    private Rigidbody grabbedRigidbody;  // 잡은 물체의 Rigidbody 컴포넌트

    void Update()
    {
        // 매 프레임마다 디버그 레이를 그림
        DrawDebugRay();

        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedObject == null)
            {
                GrabObject();  // 물체를 잡지 않은 상태라면 잡기 시도
            }
            else
            {
                PlaceObject();  // 이미 물체를 잡고 있다면 놓기
            }
        }
        // 물체를 잡고 있는 경우 지속적으로 업데이트
        if (grabbedObject != null)
        {
            UpdateObjectPosition();  // 물체 위치 업데이트
            AdjustObjectDistance();  // 물체 거리 조정 (줌)
            RotateObject();  // 물체 회전
        }
    }

    // 디버그 레이를 그리는 메서드
    void DrawDebugRay()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * grabDistance, rayColor, rayDuration);
    }

    // 물체를 잡는 메서드
    void GrabObject()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Unreal"))
            {
                grabbedObject = hit.collider.gameObject;
                originalScale = grabbedObject.transform.localScale;
                originalDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
                placeDistance = hit.distance;

                // Rigidbody 컴포넌트 가져오기 또는 추가
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody == null)
                {
                    grabbedRigidbody = grabbedObject.AddComponent<Rigidbody>();
                }
                grabbedRigidbody.useGravity = false;  // 중력 비활성화
                grabbedRigidbody.drag = 10;  // 공기 저항 증가로 부드러운 움직임

                Debug.Log($"Grabbed object: {grabbedObject.name} at distance: {placeDistance}");
            }
        }
    }

    // 잡은 물체의 위치를 업데이트하는 메서드
    void UpdateObjectPosition()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Vector3 targetPosition = ray.GetPoint(placeDistance);

        // 부드러운 이동을 위해 Lerp 사용
        Vector3 newPosition = Vector3.Lerp(grabbedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        grabbedRigidbody.MovePosition(newPosition);

        // 거리에 따른 스케일 조정 (강제 원근법 효과)
        float currentDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
        float scaleFactor = currentDistance / originalDistance;
        grabbedObject.transform.localScale = originalScale * scaleFactor;
    }

    // 마우스 휠로 물체의 거리를 조정하는 메서드
    void AdjustObjectDistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newPlaceDistance = placeDistance - scrollInput * zoomSpeed;

            // 벽 감지를 위한 레이캐스트
            RaycastHit hitInfo;
            Vector3 direction = mainCamera.transform.forward;

            // 벽과의 충돌 검사
            if (Physics.Raycast(mainCamera.transform.position, direction, out hitInfo, newPlaceDistance, wallLayer))
            {
                return;  // 벽에 부딪치면 거리 조정을 중단
            }

            // 최소/최대 거리 제한
            placeDistance = Mathf.Clamp(newPlaceDistance, minDistance, maxDistance);
        }
    }

    // 물체를 회전시키는 메서드
    void RotateObject()
    {
        if (Input.GetKey(KeyCode.E))
        {
            float rotX = -Input.GetAxis("Mouse Y") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse X") * rotationSpeed;
            Quaternion deltaRotation = Quaternion.Euler(rotX, rotY, 0f);
            grabbedRigidbody.MoveRotation(grabbedRigidbody.rotation * deltaRotation);
        }
    }

    // 물체를 놓는 메서드
    void PlaceObject()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.useGravity = true;  // 중력 다시 활성화
            grabbedRigidbody.drag = 0;  // 기본 공기 저항으로 복원
        }
        grabbedObject = null;  // 잡은 물체 참조 제거
        grabbedRigidbody = null;  // Rigidbody 참조 제거
    }
}