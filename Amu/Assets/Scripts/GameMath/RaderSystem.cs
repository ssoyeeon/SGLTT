using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderSystem : MonoBehaviour
{
    //���̴� ȸ�� �ӵ� ���� ����
    //���̴� �þ߰� ���� ���
    //������ Ÿ�ٱ����� �Ÿ� ǥ��
    //��ֹ��� ���� ���� ����

    public float detectionRadius = 10f;         //���� ����
    public float viewAngle = 90f;               //�þ߰�
    public float rotationSpeed = 100f;          //���̴� ȸ�� �ӵ�
    public float moveSpeed = 5f;                //�̵� �ӵ�

    public LayerMask targetLayer;               //Ÿ�� ���̾�
    public GameObject radarLinePrefab;          //���̴� ���� ������

    private Transform radarLine;                //���̴� ����
    private List<GameObject> detectedTargets = new List<GameObject>();

    void Start()
    {
        //���̴� ���� ����
        radarLine = Instantiate(radarLinePrefab, transform.position, Quaternion.identity).transform;
        radarLine.parent = transform;   //���̴������� �θ�� Ʈ�������̿��� �׳� ���� �ڽ����� �����ϰ� �����ſ���!
    }

    void Update()
    {
        //�̵�ó��
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //���̴� ȸ��
        radarLine.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        //Ÿ�� ����
        DetextTargets();
    }

    void DetextTargets()
    {
        //���� ���� ��� �ʱ�ȭ
        detectedTargets.Clear();

        //���� �� ��� Ÿ�� �˻�
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (Collider target in targetsInRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            //���̴� ���ΰ� Ÿ�� ���� ������ ���� ���
            float angle = Vector3.Dot(radarLine.forward, directionToTarget);

            //�þ߰� ���� �ִ��� Ȯ��
            if(angle > Mathf.Cos((viewAngle/2) * Mathf.Deg2Rad))
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, directionToTarget, out hit, detectionRadius))
                {
                    if(hit.collider.gameObject.tag == "Wall")
                    {
                        Debug.Log("����");
                        target.GetComponent<Renderer>().material.color = Color.white;
                    }
                    else
                    {
                        detectedTargets.Add(target.gameObject);
                        //�߰� Ÿ�� ǥ��
                        target.GetComponent<Renderer>().material.color = Color.red;
                        Debug.Log(Vector3.Distance(target.transform.position, this.transform.position));

                    }

                }
               
            }
            else
            {
                //�̹߰� Ÿ�� ���� ��������
                target.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    void OnDrawGizmos()
    {
        //���� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
