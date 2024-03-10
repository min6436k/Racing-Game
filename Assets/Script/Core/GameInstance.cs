using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class GameInstance : MonoBehaviour
{
    static public GameInstance instance;

    public List<BaseStage> Stages;
    public int CurrentStage;
    public float[] CurrentClearTimes = new float[3];

    public List<float> TotalRanking = new List<float>(); 

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
            Destroy(this.gameObject);
        }
    }

    public void BuyShopItem(EnumTypes.Shop ShopItem)
    {
        ShopItem TargetItem = ShopItems.Find(x => x.type == ShopItem);

        if (TargetItem.ShopItemRank < TargetItem.MaxUpgradeRank)
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

    public void AddRanking()
    {
        TotalRanking.Add(CurrentClearTimes.Sum());

        TotalRanking.Sort();

        if (TotalRanking.Count > 5)
        {
            TotalRanking.RemoveAt(TotalRanking.Count - 1);
        }

        InitGame();
    }

    public void InitGame()
    {
        CurrentClearTimes = new float[3];
        
        foreach(var i in ShopItems)
            i.ShopItemRank = 0;

        foreach(var i in Stages)
            i.Cleared = false;

        Coin = 0;
    }
}
