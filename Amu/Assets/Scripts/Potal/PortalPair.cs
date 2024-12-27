using UnityEngine;

public class PortalPair : MonoBehaviour
{
    public Portal[] Portals {  private set; get; }

    private void Awake()
    {
        Portals = GetComponentsInChildren<Portal>();

        if(Portals.Length != 2)
        {
            Debug.LogError("포탈이 2개여야만 합니다.");
        }
    }
}
