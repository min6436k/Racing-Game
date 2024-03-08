using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    public float SpeedMultiply;
    public override void GetItem()
    {
        base.GetItem();

        Rigidbody CarRigid = GameManager.instance.Player.GetComponent<Rigidbody>();
        CarRigid.velocity = CarRigid.velocity * SpeedMultiply;
    }
}
