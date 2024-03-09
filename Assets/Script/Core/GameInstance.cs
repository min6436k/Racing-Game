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
    public int CurrentStage;
    public float[] CurrentClearTimes = new float[3];

    public List<ShopItem> ShopItems;
    public int Coin = 0;

    public UnityEvent UpdateShopEvent;

    public bool bFreeShoping = false;



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
        ShopItem TargetItem = ShopItems.Find(x => x.type == ShopItem);

        if (TargetItem.ShopItemRank < 3)
        {

            if (bFreeShoping)
            {
                bFreeShoping = !bFreeShoping;
                TargetItem.ShopItemRank++;
            }
            else if (TargetItem.Price <= Coin)
            {
                Coin -= TargetItem.Price;
                TargetItem.ShopItemRank++;
            }

            UpdateShopEvent.Invoke();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2)) //상점 무료
        {
            bFreeShoping = true;
        }
    }
}
