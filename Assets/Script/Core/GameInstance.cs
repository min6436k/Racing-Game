using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    static public GameInstance instance;

    public List<BaseStage> Stages;
    public List<ShopItem> ShopItems;
    public int Coin = 0;

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

    }
    void Update()
    {
        
    }
}
