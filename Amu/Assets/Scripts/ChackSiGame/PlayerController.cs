using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //��� ���� rig false ���ѳ���
    [Header("Movement Settings")]
    public float moveSpeed = 5f;                // �÷��̾��� �̵� �ӵ�
    public float mouseSensitivity = 2f;         // ���콺 ���� (���� ȸ�� �ӵ�)
    public float jumpForce = 1.5f;              // ������ �� ����Ǵ� ��
    public float gravity = -12f;              // �߷� ���ӵ� ��

    [Header("Interaction Settings")]
    public float pickupRange = 10f;             // ��ü�� ���� �� �ִ� �ִ� �Ÿ�
    public LayerMask pickupLayer;               // ���� �� �ִ� ��ü�� ���� ���̾�
    public LayerMask wallLayer;                 // ���� ���� ���̾�
    public float placeOffset = 0.1f;            // ��ü�� ���� �� �����κ����� �Ÿ�

    [Header("Size Limits")]
    public float minSize = 0.1f;                // ��ü�� �ּ� ũ��
    public float maxSize = 10f;                 // ��ü�� �ִ� ũ��

    [Header("Distance Settings")]
    public float minDistance = 1f;              // �÷��̾�� ��ü ������ �ּ� �Ÿ�

    [Header("Smoothing Settings")]
    public float smoothSpeed = 10f;             // ��ü �̵� �� ũ�� ������ �ε巯�� ����
    public float positionPrecision = 0.001f;    // ��ġ ������ ���е�
    public float scalePrecision = 0.0001f;      // ũ�� ������ ���е�

    [Header("UI Settings")]
    public Color pickupCursorColor = Color.green;   // ��ü�� ���� �� ���� ���� Ŀ�� ����  
    public Texture2D defaultCrosshair;          // �⺻ ũ�ν���� �ؽ�ó
    public Texture2D pickupCrosshair;           // ��ü�� ���� �� ���� ���� ũ�ν���� �ؽ�ó
    public Color crosshairColor = Color.white;  // ũ�ν���� ����
    public float crosshairSize = 32f;           // ũ�ν���� ũ��

    // Private variables
    private CharacterController controller;     // �÷��̾��� CharacterController ������Ʈ
    private Camera playerCamera;                // �÷��̾� ī�޶�
    private float verticalRotation = 0f;        // ���� ȸ�� ����
    private Vector3 velocity;                   // �÷��̾��� ���� �ӵ�
    private bool isGrounded;                    // �÷��̾ ���� ��� �ִ��� ����

    private GameObject heldObject;              // ���� ��� �ִ� ��ü
    private Vector3 originalScale;              // ��� �ִ� ��ü�� ���� ũ��
    private float originalDistance;             // ��ü�� ������ ���� ���� �Ÿ�
    private Rigidbody heldRigidbody;            // ��� �ִ� ��ü�� Rigidbody ������Ʈ
    private Collider heldCollider;              // ��� �ִ� ��ü�� Collider ������Ʈ

    private Vector3 targetPosition;             // ��ü�� ��ǥ ��ġ
    private Vector3 targetScale;                // ��ü�� ��ǥ ũ��

    private Quaternion originalRotation;        // ��� �ִ� ��ü�� ���� ȸ��
   
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;          // ��ü ȸ�� �ӵ�
    public Vector3 rotationAxis = Vector3.up;   // ȸ�� �� (�⺻��: Y��)

    private Quaternion objectRotation;          // ��ü�� ���� ȸ�� ����
    private bool isRotating = false;            // ȸ�� ������ ����

    private bool canPickup = false;             // ��ü�� ���� �� �ִ��� ����

    private Vector3 objectOffset;               // ��ü �߽ɰ� �Ǻ� ����Ʈ ������ ������

    [Header("Object Holding Settings")]
    public float verticalOffset = 0f;           // ��ü�� ���� ��ġ ������ ���� ������



    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        objectRotation = Quaternion.identity;

        // Ŀ�� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleObjectInteraction();
        CheckPickupRange();
    }

    void CheckPickupRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange, pickupLayer))
        {
            canPickup = true;
        }
        else
        {
            canPickup = false;
        }
    }

    void OnGUI()
    {
        // ȭ�� �߾� ��ǥ ���
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        // ũ�ν���� ��ġ �� ũ�� ���
        Rect crosshairRect = new Rect(centerX - crosshairSize / 2, centerY - crosshairSize / 2, crosshairSize, crosshairSize);

        // ũ�ν���� �׸���
        GUI.color = crosshairColor;
        if (canPickup)
        {
            GUI.DrawTexture(crosshairRect, pickupCrosshair);
        }
        else
        {
            GUI.DrawTexture(crosshairRect, defaultCrosshair);
        }
    }


    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // ���� ó��
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -1f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleObjectInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                PickupObject();
            else
                PlaceObject();
        }

        if (heldObject != null)
        {
            UpdateObjectSizeAndPosition();
            RotateObject();
        }
    }

    void PickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange, pickupLayer))
        {
            heldObject = hit.transform.gameObject;
            originalScale = heldObject.transform.localScale;
            originalDistance = hit.distance;
            objectRotation = heldObject.transform.rotation;

            // ��ü�� �߽��� ���
            Bounds bounds = CalculateBounds(heldObject);
            objectOffset = heldObject.transform.position - bounds.center;

            heldRigidbody = heldObject.GetComponent<Rigidbody>();
            heldCollider = heldObject.GetComponent<Collider>();

            if (heldRigidbody != null)
            {
                heldRigidbody.isKinematic = true;
            }

            if (heldCollider != null)
            {
                heldCollider.enabled = false;
            }
        }
    }

    void PlaceObject()
    {
        if (heldObject == null) return;

        Vector3 currentPosition = heldObject.transform.position;
        Vector3 currentScale = heldObject.transform.localScale;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange, wallLayer))
        {
            currentPosition = hit.point + hit.normal * (currentScale.magnitude * 0.5f);
        }

        heldObject.transform.position = currentPosition;
        heldObject.transform.rotation = objectRotation;  // ���� ȸ�� ���� ����

        if (heldCollider != null)
        {
            heldCollider.enabled = true;
        }

        if (heldRigidbody != null)
        {
            heldRigidbody.isKinematic = false;
            heldRigidbody.velocity = Vector3.zero;
            heldRigidbody.angularVelocity = Vector3.zero;
        }

        heldObject = null;
        heldRigidbody = null;
        heldCollider = null;
    }

    void UpdateObjectSizeAndPosition()
    {
        if (heldObject == null) return;

        float distance = Vector3.Distance(playerCamera.transform.position, heldObject.transform.position);
        float scaleFactor = distance / originalDistance;

        targetScale = originalScale * scaleFactor;

        // �� �ະ�� �ּ�/�ִ� ũ�� ���� ����
        targetScale.x = Mathf.Clamp(targetScale.x, minSize, maxSize);
        targetScale.y = Mathf.Clamp(targetScale.y, minSize, maxSize);
        targetScale.z = Mathf.Clamp(targetScale.z, minSize, maxSize);

        // ũ�� ���е� ����
        targetScale = new Vector3(
            Mathf.Round(targetScale.x / scalePrecision) * scalePrecision,
            Mathf.Round(targetScale.y / scalePrecision) * scalePrecision,
            Mathf.Round(targetScale.z / scalePrecision) * scalePrecision
        );

        // �ε巯�� ũ�� ���� ����
        heldObject.transform.localScale = Vector3.Lerp(heldObject.transform.localScale, targetScale, smoothSpeed * Time.deltaTime);

        // ���ο� ��ġ ��� (�ּ� �Ÿ� ����)
        float newDistance = Mathf.Max(distance, minDistance);

        // ȭ�� �߾��� ���� ��ǥ ���
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, newDistance);
        Vector3 worldCenter = playerCamera.ScreenToWorldPoint(screenCenter);

        // ��ü�� ���ο� ��ġ ��� (���̸� ȭ�� �߾�����)
        targetPosition = new Vector3(
            heldObject.transform.position.x,
            worldCenter.y + verticalOffset,
            heldObject.transform.position.z
        );

        // ��ü�� �׻� ī�޶� �տ� �ֵ��� ����
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; // y �� ȸ�� ����
        targetPosition = playerCamera.transform.position + cameraForward.normalized * newDistance;

        // ���� ����
        targetPosition.y = worldCenter.y + verticalOffset;

        // ��ü�� �߽��� Ÿ�� ��ġ�� ������ ����
        targetPosition += objectOffset;

        // �� �浹 �˻�
        targetPosition = AdjustPositionForWalls(targetPosition, targetScale);

        // ��ġ ���е� ����
        targetPosition = new Vector3(
            Mathf.Round(targetPosition.x / positionPrecision) * positionPrecision,
            Mathf.Round(targetPosition.y / positionPrecision) * positionPrecision,
            Mathf.Round(targetPosition.z / positionPrecision) * positionPrecision
        );

        // �ε巯�� ��ġ ���� ����
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // ��ü�� ȸ�� ���� ����
        heldObject.transform.rotation = objectRotation;
    }

    Bounds CalculateBounds(GameObject obj)
    {
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }

    Vector3 AdjustPositionForWalls(Vector3 desiredPosition, Vector3 objectScale)
    {
        float radius = Mathf.Max(objectScale.x, objectScale.y, objectScale.z) / 2;
        RaycastHit hit;
        if (Physics.SphereCast(playerCamera.transform.position, radius, playerCamera.transform.forward, out hit, pickupRange, wallLayer))
        {
            return hit.point + hit.normal * (radius + placeOffset);
        }
        return desiredPosition;
    }

    void RotateObject()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRotating = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // R Ű�� ������ �ִ� ���� ��ü�� ���� ��ġ���� ����
            float rotationAmount = rotationSpeed * Time.deltaTime;
            objectRotation *= Quaternion.AngleAxis(rotationAmount, rotationAxis);
        }
    }


}