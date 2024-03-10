using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashparticle : MonoBehaviour
{
    public GameObject Prefap;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameObject instance = Instantiate(Prefap, collision.contacts[0].point, Quaternion.identity);
            Destroy(instance, 3);
        }
    }
}
