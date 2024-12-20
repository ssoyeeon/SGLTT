using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderSystem : MonoBehaviour
{
    //레이더 회전 속도 조절 가능
    //레이더 시야각 조절 기능
    //감지된 타겟까지의 거리 표시
    //장애물에 의한 차단 구현

    public float detectionRadius = 10f;         //감지 범위
    public float viewAngle = 90f;               //시야각
    public float rotationSpeed = 100f;          //레이더 회전 속도
    public float moveSpeed = 5f;                //이동 속도

    public LayerMask targetLayer;               //타겟 레이어
    public GameObject radarLinePrefab;          //레이더 라인 프리팹

    private Transform radarLine;                //레이더 라인
    private List<GameObject> detectedTargets = new List<GameObject>();

    void Start()
    {
        //레이더 라인 생성
        radarLine = Instantiate(radarLinePrefab, transform.position, Quaternion.identity).transform;
        radarLine.parent = transform;   //레이더라인의 부모는 트랜스폼이에요 그냥 본인 자식으로 설정하고 싶은거에욧!
    }

    void Update()
    {
        //이동처리
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //레이더 회전
        radarLine.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        //타겟 감지
        DetextTargets();
    }

    void DetextTargets()
    {
        //이전 감지 목록 초기화
        detectedTargets.Clear();

        //범위 내 모든 타겟 검사
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (Collider target in targetsInRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            //레이더 라인과 타겟 방향 사이의 각도 계산
            float angle = Vector3.Dot(radarLine.forward, directionToTarget);

            //시야각 내에 있는지 확인
            if(angle > Mathf.Cos((viewAngle/2) * Mathf.Deg2Rad))
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, directionToTarget, out hit, detectionRadius))
                {
                    if(hit.collider.gameObject.tag == "Wall")
                    {
                        Debug.Log("막힘");
                        target.GetComponent<Renderer>().material.color = Color.white;
                    }
                    else
                    {
                        detectedTargets.Add(target.gameObject);
                        //발견 타겟 표시
                        target.GetComponent<Renderer>().material.color = Color.red;
                        Debug.Log(Vector3.Distance(target.transform.position, this.transform.position));

                    }

                }
               
            }
            else
            {
                //미발견 타겟 원래 색상으로
                target.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    void OnDrawGizmos()
    {
        //감지 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
