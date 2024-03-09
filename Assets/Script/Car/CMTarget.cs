using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMTarget : MonoBehaviour
{
    public Transform PlayerTF;
    void Start()
    {
    }

    void Update()
    {
        transform.position = PlayerTF.position;
        transform.localPosition += new Vector3(0, 0.5f, -0.5f);
        transform.rotation = Quaternion.Lerp(transform.rotation, PlayerTF.rotation, 6f * Time.deltaTime);
    }
}
