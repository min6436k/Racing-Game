using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;

    public Vector3 Distance;

    public bool BisCamera;
    void Update()
    {
        Vector3 F = Target.forward;
        F.y = Distance.y;

        transform.position = Target.position + Distance;


        if (BisCamera) transform.rotation = Quaternion.LookRotation(F, Vector3.up) * Quaternion.Euler(new Vector3(180, 0, 0));
        else transform.rotation = Quaternion.LookRotation(F, Vector3.up) * Quaternion.Euler(new Vector3(0, 0, 180));

    }
}
