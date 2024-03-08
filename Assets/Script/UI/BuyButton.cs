using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public EnumTypes.Shop ShopItem;
    public void OnBuyButton()
    {
        GameInstance.instance.BuyShopItem(ShopItem);
    }
}
