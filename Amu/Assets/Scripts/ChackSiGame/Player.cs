using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float moveSpeed = 5f;
    public Texture2D cursorTexture; // Inspector���� Ŀ�� �̹����� �Ҵ��ؾ� �մϴ�.
    private float verticalRotation = 0f;
    private CharacterController controller;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.identity;
        }
        // �÷��̾��� �ʱ� ȸ�� ����
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // Ŀ���� ����� �߾ӿ� ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void OnGUI()
    {
        // ȭ�� �߾ӿ� Ŀ���� Ŀ�� �׸���
        if (cursorTexture != null)
        {
            float xPos = Screen.width / 2 - cursorTexture.width / 2;
            float yPos = Screen.height / 2 - cursorTexture.height / 2;
            GUI.DrawTexture(new Rect(xPos, yPos, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // �÷��̾��� �¿� ȸ��
        transform.Rotate(Vector3.up * mouseX);

        // ī�޶��� ���� ȸ��
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }
}