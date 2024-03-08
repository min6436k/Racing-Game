using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ShopItem
{
    public int ShopItemRank;

    [Header("아래는 GameInstance에서 건드리는거 아님")]
    public EnumTypes.Shop type;

    public Transform Ranks;

}
public class ShopManager : MonoBehaviour
{
    public List<ShopItem> ShopItems = new List<ShopItem> ();

    private void Start()
    {
        UpdateShopUI();
        GameInstance.instance.UpdateShopEvent.AddListener(()=>UpdateShopUI());
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
