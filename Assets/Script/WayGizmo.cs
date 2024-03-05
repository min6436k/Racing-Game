using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayGizmo : MonoBehaviour
{
    public Transform WayPoints;
    private void OnDrawGizmos()
    {
        foreach (Transform t in WayPoints)
        {
            Gizmos.DrawWireSphere(t.position, 1);
        }

        Gizmos.DrawLine(WayPoints.GetChild(0).position, WayPoints.GetChild(WayPoints.childCount-1).position);

        for(int i = 0; i < WayPoints.childCount-1; ++i)
        {
            Gizmos.DrawLine(WayPoints.GetChild(i).position, WayPoints.GetChild(i+1).position);
        }
    }

}