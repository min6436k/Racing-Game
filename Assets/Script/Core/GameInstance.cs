using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class GameInstance : MonoBehaviour
{
    static public GameInstance instance;

    public List<BaseStage> Stages;
    public List<ShopItem> ShopItems;
    public int Coin = 0;

    public UnityEvent UpdateShopEvent;


    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void BuyShopItem(EnumTypes.Shop ShopItem)
    {
        ShopItem tempClass = ShopItems.Find(x => x.type == ShopItem);

        if (tempClass.ShopItemRank < 3)
        {
            tempClass.ShopItemRank++;
            UpdateShopEvent.Invoke();
        }
    }
    void Update()
    {
        
    }
}
