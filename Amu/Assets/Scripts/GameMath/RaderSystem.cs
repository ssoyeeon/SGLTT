using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderSystem : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float viewAngle = 90f;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;

    public LayerMask targetLayer;
    public GameObject radarLinePrefab;

    private Transform radarLine;
    private List<GameObject> detectedTargets = new List<GameObject>();



    void Start()
    {
        radarLine = Instantiate(radarLinePrefab, transform.position, Quaternion.identity).transform;
        radarLine.parent = transform;   //레이더라인의 부모는 트랜스폼이에요 어쩌라고?
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        radarLine.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        DetextTargets();
    }

    void DetextTargets()
    {
        detectedTargets.Clear();

        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (Collider target in targetsInRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            float angle = Vector3.Dot(radarLine.forward, directionToTarget);

            if(angle > Mathf.Cos((viewAngle/2) * Mathf.Deg2Rad))
            {
                detectedTargets.Add(target.gameObject);
                target.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                target.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
