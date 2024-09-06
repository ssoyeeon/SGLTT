using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float moveSpeed = 5f;
    public Texture2D cursorTexture; // Inspector에서 커서 이미지를 할당해야 합니다.
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
        // 플레이어의 초기 회전 설정
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // 커서를 숨기고 중앙에 고정
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
        // 화면 중앙에 커스텀 커서 그리기
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

        // 플레이어의 좌우 회전
        transform.Rotate(Vector3.up * mouseX);

        // 카메라의 상하 회전
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