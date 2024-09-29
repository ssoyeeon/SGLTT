using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float interactionDistance = 10f;
    public LayerMask interactableLayer; // ��ȣ�ۿ� ������ ���̾� ����ũ
    public LayerMask wallLayer; // �� ���̾� ����ũ
    public LayerMask floorLayer; // �ٴ� ���̾� ����ũ
    public float minHoldDistance = 2f; // ��ü�� ��� ���� �� �ּ� �Ÿ�
    public float maxHoldDistance = 10f; // ��ü�� ��� ���� �� �ִ� �Ÿ�
    public float floorCheckDistance = 0.5f; // �ٴڰ��� �浹�� üũ�� �Ÿ�
    public float shrinkScaleFactor = 0.25f; // �ٴڰ� ��������� ũ�⸦ ���̴� ����
    public float floorBuffer = 0.1f; // �ٴڰ� ��ġ�� �ʵ��� �ϴ� ���� ����

    private float holdDistance = 3f; // ���� ��ü�� �÷��̾� ������ �Ÿ� (�������� ��ȭ)
    private bool isHoldingObject = false; // ��ü�� ��� �ִ��� ����

    private CharacterController controller;
    private float pitch = 0f;
    private Vector3 velocity;
    private GameObject selectedObject;
    private Rigidbody selectedObjectRb;
    private Vector3 initialScale;
    private float initialDistance;

    public float rotationSpeed = 100f;  // ��ü ȸ�� �ӵ�

    // Ŀ�� �̹���
    public Texture2D cursorImage; // ����� Ŀ�� �̹���
    private Vector2 cursorSize = new Vector2(32, 32); // Ŀ�� �̹��� ũ��

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // �⺻ Ŀ�� �����
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();

        if (isHoldingObject)
        {
            AdjustObjectScale(); // ��ü ũ�� ����
            CheckCollisionWithWall(); // �� �浹 �˻�
            CheckProximityToFloor();  // �ٴڰ��� �Ÿ� �˻�

            HandleObjectRotation();  // ��ü ȸ�� ó��
        }
    }

    void OnGUI()
    {
        // Ŀ���� ȭ�� �߾ӿ� ǥ��
        if (cursorImage != null)
        {
            // ȭ�� �߾� ��ġ ���
            float centerX = (Screen.width - cursorSize.x) / 2;
            float centerY = (Screen.height - cursorSize.y) / 2;

            // Ŀ�� �̹��� �׸���
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

        // ��ü���� �Ÿ��� ī�޶��� ���� ȸ���� ���� ����
        AdjustHoldDistance(mouseY);
    }

    void AdjustHoldDistance(float mouseY)
    {
        if (selectedObject != null)
        {
            // ���콺 Y �� �̵��� ���� holdDistance ���� (���� ���� ��ü�� �־����� Ŀ��, �Ʒ��� ���� �������)
            holdDistance += mouseY * Time.deltaTime * 5f;  // ���콺 �̵��� ���� �Ÿ� ���� �ӵ� ����
            holdDistance = Mathf.Clamp(holdDistance, minHoldDistance, maxHoldDistance);  // �ּ�/�ִ� �Ÿ� ����
        }
    }

    void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))  // When the player clicks the mouse.
        {
            if (selectedObject == null)
            {
                TrySelectObject();  // ��ü�� �����ؼ� ���
            }
            else
            {
                DropObject();  // ��ü�� ���´�
            }
        }

        if (selectedObject != null)
        {
            HoldObject();  // ��ü�� ��� ���� �� ��ġ ����
        }
    }

    void TrySelectObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // �ش� ���̾ ����
        {
            selectedObject = hit.collider.gameObject;
            selectedObjectRb = selectedObject.GetComponent<Rigidbody>();

            if (selectedObjectRb != null)
            {
                selectedObjectRb.isKinematic = true;  // ��ü�� �� ���¿����� ���� ���� ������ ���� �ʰ� �Ѵ�.
            }

            // ��ü�� ó�� ���� �� �÷��̾�� ��ü ������ �Ÿ��� �������� holdDistance ����
            holdDistance = Vector3.Distance(playerCamera.transform.position, selectedObject.transform.position);

            // ��ü ũ�� ���� �ʱ� �� ����
            initialScale = selectedObject.transform.localScale;
            initialDistance = holdDistance;

            isHoldingObject = true;
        }
    }

    void HoldObject()
    {
        if (selectedObject != null)
        {
            // ��ü�� ī�޶� �� ������ �Ÿ��� �ֵ��� ���� (holdDistance ���� ���� ��ġ ����)
            selectedObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
        }
    }

    void AdjustObjectScale()
    {
        if (selectedObject != null)
        {
            // ���� �Ÿ� ���
            float currentDistance = Vector3.Distance(playerCamera.transform.position, selectedObject.transform.position);
            float scaleFactor = currentDistance / initialDistance;  // ��ü�� �־������� Ŀ���� ����������� �۾���

            // ��ǥ ũ�� ���
            Vector3 targetScale = initialScale * scaleFactor;

            // �ּ� ũ�� 0.1�� ����
            targetScale = Vector3.Max(targetScale, new Vector3(0.1f, 0.1f, 0.1f));

            // ��ü�� �������� �ε巴�� ����
            selectedObject.transform.localScale = Vector3.Lerp(selectedObject.transform.localScale, targetScale, Time.deltaTime * 5f);
        }
    }


    void DropObject()
    {
        if (selectedObjectRb != null)
        {
            selectedObjectRb.isKinematic = false;  // �ٽ� ���� ������ ������ �ް� �Ѵ�.
        }

        selectedObject = null;  // ������ ��ü�� ����
        selectedObjectRb = null;
        isHoldingObject = false;  // ��ü�� ��� ���� ���� ���·� ����
    }

    // ���� �浹�ϸ� ��ü�� �÷��̾� ������ ���鼭 ũ�⸦ ����
    void CheckCollisionWithWall()
    {
        if (selectedObject != null)
        {
            // SphereCast�� ������ �浹�� üũ
            float objectRadius = Mathf.Max(selectedObject.transform.localScale.x, selectedObject.transform.localScale.y, selectedObject.transform.localScale.z) / 2f;
            RaycastHit hit;

            // ĳ���Ͱ� ��ü�� ������ �̵���ų �� ���� �浹�� üũ
            if (Physics.SphereCast(playerCamera.transform.position, objectRadius, playerCamera.transform.forward, out hit, holdDistance, wallLayer))
            {
                // ���� �浹�� �����Ǹ� ��ü�� �ڵ����� �������´�
                Debug.Log("���� �浹! ��ü�� ���������ϴ�.");
                DropObject();  // ���� �浹 �� ��ü�� ��������
            }
        }
    }

    // R Ű�� ������ ���� �� ��ü ȸ�� ó��
    void HandleObjectRotation()
    {
        if (Input.GetKey(KeyCode.R) && selectedObject != null)
        {
            // ��ü�� ȸ����Ŵ (Y���� �������� ȸ��)
            selectedObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // �ٴڰ� ��������� ũ�� ���� �� ��� ����
    void CheckProximityToFloor()
    {
        if (selectedObject != null)
        {
            RaycastHit hit;
            // �Ʒ������� Raycast�� ���� �ٴڰ��� �Ÿ��� ���
            if (Physics.Raycast(selectedObject.transform.position, Vector3.down, out hit, floorCheckDistance, floorLayer))
            {
                // ��ü�� �ٴڿ� �ʹ� ��������� ��ü�� �ڵ����� ��������
                if (hit.distance < floorBuffer)
                {
                    Debug.Log("�ٴڿ� ������Ƿ� ��ü�� ���������ϴ�.");
                    DropObject();  // ��ü�� �ڵ����� ��������
                    return;  // �浹 �� �� �̻� �������� �ʵ��� ����
                }

                // ī�޶� �ٴ��� ���ϰ� ���� ���� ũ�� ����
                Vector3 cameraForward = playerCamera.transform.forward;
                Vector3 floorNormal = hit.normal;

                // ī�޶� �ٴ��� ���� ��
                if (Vector3.Dot(cameraForward, floorNormal) < 0)
                {
                    // �ٴڿ� ����������� ��ü ũ�⸦ ����
                    float distanceToFloor = hit.distance;
                    float targetScaleFactor = 1f - (1f - shrinkScaleFactor) * (1f - (distanceToFloor / floorCheckDistance));

                    // ��ǥ ũ�� ���
                    Vector3 targetScale = initialScale * Mathf.Clamp(targetScaleFactor, 0.1f, 1f); // �ּ� ũ�� 0.1�� ����

                    // ��ü�� �������� �ε巴�� ����
                    selectedObject.transform.localScale = Vector3.Lerp(selectedObject.transform.localScale, targetScale, Time.deltaTime * 5f);
                }
            }
        }
    }



}
