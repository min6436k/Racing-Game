using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCrach_SpeedUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Rigidbody>().AddForce(transform.forward * 10000, ForceMode.Impulse);
        }
    }
}
