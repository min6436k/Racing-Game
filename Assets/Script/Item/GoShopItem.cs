using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoShopItem : BaseItem
{
    public override void GetItem()
    {
        base.GetItem();

        GameManager.instance.GoShop();
        Destroy(gameObject);
    }
}
