using UnityEngine;

public class ForcedPerspective : MonoBehaviour
{
    public Camera mainCamera;          // ���� ī�޶� ����
    public float grabDistance = 100f;  // ��ü�� ���� �� �ִ� �ִ� �Ÿ�
    public float placeDistance = 10f;  // ��ü�� ���� �⺻ �Ÿ�
    public float zoomSpeed = 1f;       // �� �ӵ� (���콺 �� ����)
    public float minDistance = 1f;     // ��ü�� ������ �� �ִ� �ּ� �Ÿ�
    public float maxDistance = 20f;    // ��ü�� ���� �� �ִ� �ִ� �Ÿ�
    public float force = 10f;          // ��ü�� ���� ��
    public bool isGrab = false;        // ��ü�� ����ִ���
    public Rigidbody rig;              // ��ü�� ���� �� ������ٵ�
    public float speed = 20f;          // ��ü ������¡

    private GameObject grabbedObject;  // ���� ��� �ִ� ��ü
    private Vector3 originalScale;     // ��ü�� ���� ũ��
    private float originalDistance;    // ��ü�� ����� ���� ���� �Ÿ�

    void Update()
    {
        // ���� ���콺 ��ư�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            if ( grabbedObject == null )
            {              
                GrabObject();  // ��ü�� ����
                isGrab = true;
            }
            else
            {
                PlaceObject(); // ��ü�� ����
                isGrab = false;
            }
        }

        // ��ü�� ��� �ִ� ���
        if (grabbedObject != null)
        {
            UpdateObjectPosition();  // ��ü ��ġ ������Ʈ
            AdjustObjectDistance();  // ��ü �Ÿ� ����
            isGrab = true;
        }
    }

    void GrabObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // ����ĳ��Ʈ�� ��ü ����
        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if(hit.collider.gameObject.tag == "Unreal")
            {
                grabbedObject = hit.collider.gameObject;
                originalScale = grabbedObject.transform.localScale;
                originalDistance = Vector3.Distance(mainCamera.transform.position, grabbedObject.transform.position);
                placeDistance = Mathf.Min(hit.distance, placeDistance); // �ʱ� �Ÿ� ����
            }           
        }
    }

    void UpdateObjectPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 newPosition = ray.GetPoint(placeDistance);
        grabbedObject.transform.position = newPosition;

        // �Ÿ��� ���� ������ ����
        float currentDistance = Vector3.Distance(mainCamera.transform.position, newPosition);
        float scaleFactor = currentDistance / originalDistance;
        grabbedObject.transform.localScale = originalScale * scaleFactor;

        // ��ü ȸ��
        if (Input.GetKey(KeyCode.E))        
        {
            grabbedObject.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
            grabbedObject.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);              
        }
    }

    void AdjustObjectDistance()
    {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                // ���콺 �� �Է¿� ���� �Ÿ� ����
                placeDistance -= scrollInput * zoomSpeed;
                // �Ÿ��� �ּҰ��� �ִ밪 ���̷� ����
                placeDistance = Mathf.Clamp(placeDistance, minDistance, maxDistance);
            }
    }

    void PlaceObject()
    {
        // ��ü�� ���� (��� �ִ� ��ü ���� ����)
        grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabbedObject = null;

    }
}