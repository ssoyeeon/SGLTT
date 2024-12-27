using UnityEngine;

public class PortalPair : MonoBehaviour
{
    public Portal[] Portals {  private set; get; }

    private void Awake()
    {
        Portals = GetComponentsInChildren<Portal>();

        if(Portals.Length != 2)
        {
            Debug.LogError("��Ż�� 2�����߸� �մϴ�.");
        }
    }
}
