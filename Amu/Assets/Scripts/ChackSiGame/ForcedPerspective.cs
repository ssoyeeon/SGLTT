using UnityEngine;

public class ForcedPerspective : MonoBehaviour
{
    // ���� ������: Unity Inspector���� ���� ����
    public Camera mainCamera;  // ���� ī�޶� ����
    public float grabDistance = 100f;  // ��ü�� ���� �� �ִ� �ִ� �Ÿ�
    public float placeDistance = 10f;  // ��ü�� ���� �⺻ �Ÿ�
    public float zoomSpeed = 1f;  // �� �ӵ� (��ũ�� �ΰ���)
    public float minDistance = 1f;  // ��ü�� ī�޶� ������ �� �ִ� �ּ� �Ÿ�
    public float maxDistance = 20f;  // ��ü�� ī�޶󿡼� �ָ� �� �� �ִ� �ִ� �Ÿ�
    public float rotationSpeed = 20f;  // ��ü ȸ�� �ӵ�
    public float moveSpeed = 10f;  // ��ü �̵� �ӵ�
    public LayerMask wallLayer;  // ������ ������ ���̾�
    public Color rayColor = Color.red;  // ����� ������ ����
    public float rayDuration = 0.1f;  // ����� ���̰� ���̴� �ð�

    // ����� ������: ���� ������ ���
    private GameObject grabbedObject;  // ���� ��� �ִ� ��ü
    private Vector3 originalScale;  // ��ü�� ���� ũ��
    private float originalDistance;  // ��ü�� ����� ���� ���� �Ÿ�
    private Rigidbody grabbedRigidbody;  // ���� ��ü�� Rigidbody ������Ʈ

    void Update()
    {
        // �� �����Ӹ��� ����� ���̸� �׸�
        DrawDebugRay();

        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedObject == null)
            {
                GrabObject();  // ��ü�� ���� ���� ���¶�� ��� �õ�
            }
            else
            {
                PlaceObject();  // �̹� ��ü�� ��� �ִٸ� ����
            }
        }
        // ��ü�� ��� �ִ� ��� ���������� ������Ʈ
        if (grabbedObject != null)
        {
            UpdateObjectPosition();  // ��ü ��ġ ������Ʈ
            AdjustObjectDistance();  // ��ü �Ÿ� ���� (��)
            RotateObject();  // ��ü ȸ��
        }
    }

    // ����� ���̸� �׸��� �޼���
    void DrawDebugRay()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * grabDistance, rayColor, rayDuration);
    }

    // ��ü�� ��� �޼���
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

                // Rigidbody ������Ʈ �������� �Ǵ� �߰�
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody == null)
                {
                    grabbedRigidbody = grabbedObject.AddComponent<Rigidbody>();
                }
                grabbedRigidbody.useGravity = false;  // �߷� ��Ȱ��ȭ
                grabbedRigidbody.drag = 10;  // ���� ���� ������ �ε巯�� ������

                Debug.Log($"Grabbed object: {grabbedObject.name} at distance: {placeDistance}");
            }
        }
    }

    // ���� ��ü�� ��ġ�� ������Ʈ�ϴ� �޼���
    void UpdateObjectPosition()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Vector3 targetPosition = ray.GetPoint(placeDistance);

        // �ε巯�� �̵��� ���� Lerp ���
        Vector3 newPosition = Vector3.Lerp(grabbedObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        grabbedRigidbody.MovePosition(newPosition);

        // �Ÿ��� ���� ������ ���� (���� ���ٹ� ȿ��)
        float currentDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
        float scaleFactor = currentDistance / originalDistance;
        grabbedObject.transform.localScale = originalScale * scaleFactor;
    }

    // ���콺 �ٷ� ��ü�� �Ÿ��� �����ϴ� �޼���
    void AdjustObjectDistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newPlaceDistance = placeDistance - scrollInput * zoomSpeed;

            // �� ������ ���� ����ĳ��Ʈ
            RaycastHit hitInfo;
            Vector3 direction = mainCamera.transform.forward;

            // ������ �浹 �˻�
            if (Physics.Raycast(mainCamera.transform.position, direction, out hitInfo, newPlaceDistance, wallLayer))
            {
                return;  // ���� �ε�ġ�� �Ÿ� ������ �ߴ�
            }

            // �ּ�/�ִ� �Ÿ� ����
            placeDistance = Mathf.Clamp(newPlaceDistance, minDistance, maxDistance);
        }
    }

    // ��ü�� ȸ����Ű�� �޼���
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

    // ��ü�� ���� �޼���
    void PlaceObject()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.useGravity = true;  // �߷� �ٽ� Ȱ��ȭ
            grabbedRigidbody.drag = 0;  // �⺻ ���� �������� ����
        }
        grabbedObject = null;  // ���� ��ü ���� ����
        grabbedRigidbody = null;  // Rigidbody ���� ����
    }
}