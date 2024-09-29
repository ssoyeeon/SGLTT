using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float interactionDistance = 10f;
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 마스크
    public LayerMask wallLayer; // 벽 레이어 마스크
    public LayerMask floorLayer; // 바닥 레이어 마스크
    public float minHoldDistance = 2f; // 물체를 들고 있을 때 최소 거리
    public float maxHoldDistance = 10f; // 물체를 들고 있을 때 최대 거리
    public float floorCheckDistance = 0.5f; // 바닥과의 충돌을 체크할 거리
    public float shrinkScaleFactor = 0.25f; // 바닥과 가까워지면 크기를 줄이는 비율
    public float floorBuffer = 0.1f; // 바닥과 겹치지 않도록 하는 여유 공간

    private float holdDistance = 3f; // 현재 물체와 플레이어 사이의 거리 (동적으로 변화)
    private bool isHoldingObject = false; // 물체를 들고 있는지 여부

    private CharacterController controller;
    private float pitch = 0f;
    private Vector3 velocity;
    private GameObject selectedObject;
    private Rigidbody selectedObjectRb;
    private Vector3 initialScale;
    private float initialDistance;

    public float rotationSpeed = 100f;  // 물체 회전 속도

    // 커서 이미지
    public Texture2D cursorImage; // 사용할 커서 이미지
    private Vector2 cursorSize = new Vector2(32, 32); // 커서 이미지 크기

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // 기본 커서 숨기기
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();

        if (isHoldingObject)
        {
            AdjustObjectScale(); // 물체 크기 조정
            CheckCollisionWithWall(); // 벽 충돌 검사
            CheckProximityToFloor();  // 바닥과의 거리 검사

            HandleObjectRotation();  // 물체 회전 처리
        }
    }

    void OnGUI()
    {
        // 커서를 화면 중앙에 표시
        if (cursorImage != null)
        {
            // 화면 중앙 위치 계산
            float centerX = (Screen.width - cursorSize.x) / 2;
            float centerY = (Screen.height - cursorSize.y) / 2;

            // 커서 이미지 그리기
            GUI.DrawTexture(new Rect(centerX, centerY, cursorSize.x, cursorSize.y), cursorImage);
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Ensure the player stays on the ground.
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Jumping mechanics.
        }

        velocity.y += gravity * Time.deltaTime;  // Apply gravity.
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);  // Limit the camera's vertical rotation.
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);  // Horizontal rotation.

        // 물체와의 거리를 카메라의 수직 회전에 따라 조정
        AdjustHoldDistance(mouseY);
    }

    void AdjustHoldDistance(float mouseY)
    {
        if (selectedObject != null)
        {
            // 마우스 Y 축 이동에 따라 holdDistance 조정 (위로 가면 물체가 멀어지고 커짐, 아래로 가면 가까워짐)
            holdDistance += mouseY * Time.deltaTime * 5f;  // 마우스 이동에 따라 거리 조정 속도 설정
            holdDistance = Mathf.Clamp(holdDistance, minHoldDistance, maxHoldDistance);  // 최소/최대 거리 제한
        }
    }

    void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))  // When the player clicks the mouse.
        {
            if (selectedObject == null)
            {
                TrySelectObject();  // 물체를 선택해서 든다
            }
            else
            {
                DropObject();  // 물체를 놓는다
            }
        }

        if (selectedObject != null)
        {
            HoldObject();  // 물체를 들고 있을 때 위치 고정
        }
    }

    void TrySelectObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // 해당 레이어만 선택
        {
            selectedObject = hit.collider.gameObject;
            selectedObjectRb = selectedObject.GetComponent<Rigidbody>();

            if (selectedObjectRb != null)
            {
                selectedObjectRb.isKinematic = true;  // 물체를 든 상태에서는 물리 엔진 영향을 받지 않게 한다.
            }

            // 물체를 처음 집을 때 플레이어와 물체 사이의 거리를 기준으로 holdDistance 설정
            holdDistance = Vector3.Distance(playerCamera.transform.position, selectedObject.transform.position);

            // 물체 크기 관련 초기 값 저장
            initialScale = selectedObject.transform.localScale;
            initialDistance = holdDistance;

            isHoldingObject = true;
        }
    }

    void HoldObject()
    {
        if (selectedObject != null)
        {
            // 물체가 카메라 앞 고정된 거리에 있도록 설정 (holdDistance 값에 따라 위치 조정)
            selectedObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
        }
    }

    void AdjustObjectScale()
    {
        if (selectedObject != null)
        {
            // 현재 거리 계산
            float currentDistance = Vector3.Distance(playerCamera.transform.position, selectedObject.transform.position);
            float scaleFactor = currentDistance / initialDistance;  // 물체가 멀어질수록 커지고 가까워질수록 작아짐

            // 목표 크기 계산
            Vector3 targetScale = initialScale * scaleFactor;

            // 최소 크기 0.1로 설정
            targetScale = Vector3.Max(targetScale, new Vector3(0.1f, 0.1f, 0.1f));

            // 물체의 스케일을 부드럽게 조정
            selectedObject.transform.localScale = Vector3.Lerp(selectedObject.transform.localScale, targetScale, Time.deltaTime * 5f);
        }
    }


    void DropObject()
    {
        if (selectedObjectRb != null)
        {
            selectedObjectRb.isKinematic = false;  // 다시 물리 엔진의 영향을 받게 한다.
        }

        selectedObject = null;  // 선택한 물체를 해제
        selectedObjectRb = null;
        isHoldingObject = false;  // 물체를 들고 있지 않은 상태로 변경
    }

    // 벽에 충돌하면 물체를 플레이어 쪽으로 당기면서 크기를 줄임
    void CheckCollisionWithWall()
    {
        if (selectedObject != null)
        {
            // SphereCast로 벽과의 충돌을 체크
            float objectRadius = Mathf.Max(selectedObject.transform.localScale.x, selectedObject.transform.localScale.y, selectedObject.transform.localScale.z) / 2f;
            RaycastHit hit;

            // 캐릭터가 물체를 앞으로 이동시킬 때 벽과 충돌을 체크
            if (Physics.SphereCast(playerCamera.transform.position, objectRadius, playerCamera.transform.forward, out hit, holdDistance, wallLayer))
            {
                // 벽과 충돌이 감지되면 물체를 자동으로 내려놓는다
                Debug.Log("벽과 충돌! 물체를 내려놓습니다.");
                DropObject();  // 벽과 충돌 시 물체를 내려놓기
            }
        }
    }

    // R 키를 누르고 있을 때 물체 회전 처리
    void HandleObjectRotation()
    {
        if (Input.GetKey(KeyCode.R) && selectedObject != null)
        {
            // 물체를 회전시킴 (Y축을 기준으로 회전)
            selectedObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // 바닥과 가까워지면 크기 조정 및 통과 방지
    void CheckProximityToFloor()
    {
        if (selectedObject != null)
        {
            RaycastHit hit;
            // 아래쪽으로 Raycast를 쏴서 바닥과의 거리를 계산
            if (Physics.Raycast(selectedObject.transform.position, Vector3.down, out hit, floorCheckDistance, floorLayer))
            {
                // 물체가 바닥에 너무 가까워지면 물체를 자동으로 내려놓음
                if (hit.distance < floorBuffer)
                {
                    Debug.Log("바닥에 닿았으므로 물체를 내려놓습니다.");
                    DropObject();  // 물체를 자동으로 내려놓기
                    return;  // 충돌 시 더 이상 진행하지 않도록 종료
                }

                // 카메라가 바닥을 향하고 있을 때만 크기 조정
                Vector3 cameraForward = playerCamera.transform.forward;
                Vector3 floorNormal = hit.normal;

                // 카메라가 바닥을 향할 때
                if (Vector3.Dot(cameraForward, floorNormal) < 0)
                {
                    // 바닥에 가까워질수록 물체 크기를 줄임
                    float distanceToFloor = hit.distance;
                    float targetScaleFactor = 1f - (1f - shrinkScaleFactor) * (1f - (distanceToFloor / floorCheckDistance));

                    // 목표 크기 계산
                    Vector3 targetScale = initialScale * Mathf.Clamp(targetScaleFactor, 0.1f, 1f); // 최소 크기 0.1로 설정

                    // 물체의 스케일을 부드럽게 조정
                    selectedObject.transform.localScale = Vector3.Lerp(selectedObject.transform.localScale, targetScale, Time.deltaTime * 5f);
                }
            }
        }
    }



}
