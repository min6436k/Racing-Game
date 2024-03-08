using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ShopItem
{
    public EnumTypes.Shop type;

    [Header("인스펙터 창에서는 필요 없음")]
    public Transform Ranks;

    public int ShopItemRank;
}
public class ShopManager : MonoBehaviour
{
    public List<ShopItem> ShopItems = new List<ShopItem> ();
    private void Start()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        foreach (var item in ShopItems)
        {
            ViewUpgradeRank(GameInstance.instance.ShopItems.Find(x => x.type == item.type).ShopItemRank, item.Ranks);
        }
    }

    public void ViewUpgradeRank(int UpgradeRank, Transform RankUI)
    {
        for (int i = 0; i < RankUI.childCount; i++)
        {
            if (UpgradeRank > i) RankUI.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }
}
