using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPortalSystem : MonoBehaviour
{
    public Transform exitPortal;
    public Transform playerObject;
    public bool isMirrorWorld = false;

    private Matrix4x4 mirrorMatrix = Matrix4x4.Scale(new Vector3(-1, 1, 1));        //거울 변환 행렬

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == playerObject)
        {
            TransportPlayer();
        }
    }

    void TransportPlayer()
    {
        Matrix4x4 portalToWorld = transform.localToWorldMatrix;
        Matrix4x4 worldToPortal = transform.worldToLocalMatrix;

        Vector3 playerLocalPos = worldToPortal.MultiplyPoint3x4(playerObject.position);
        Vector3 playerLocalDir = worldToPortal.MultiplyVector(playerObject.forward);

        if(!isMirrorWorld)
        {
            playerLocalPos = mirrorMatrix.MultiplyPoint3x4(playerLocalPos);
            playerLocalDir = mirrorMatrix.MultiplyVector(playerLocalDir);
        }

        Vector3 newWorldPos = exitPortal.TransformPoint(playerLocalPos);
        Vector3 newWorldDir = exitPortal.TransformDirection(playerLocalDir);

        playerObject.position = newWorldPos;
        playerObject.rotation = Quaternion.LookRotation(newWorldDir, Vector3.up);
    }
}
