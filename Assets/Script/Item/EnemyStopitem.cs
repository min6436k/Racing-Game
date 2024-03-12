using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopitem : BaseItem
{
    public override void GetItem()
    {
        base.GetItem();

        foreach(var i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Debug.Log("a");
            if(i.TryGetComponent(out Rigidbody rb))
            {
                Debug.Log("B");

                rb.velocity = Vector3.zero;
            }
        }
        Destroy(gameObject);

    }
}
