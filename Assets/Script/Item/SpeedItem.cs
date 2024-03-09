using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    public float AddSpeed;
    public override void GetItem()
    {
        base.GetItem();

        Rigidbody CarRigid = GameManager.instance.Player.GetComponent<Rigidbody>();

        CarRigid.AddForce(GameManager.instance.Player.transform.forward * AddSpeed, ForceMode.Impulse);


        Destroy(gameObject);
    }
}
