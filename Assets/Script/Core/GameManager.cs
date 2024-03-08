using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject Managers;

    public GameObject ShopUI;

    [HideInInspector]
    public TimeGauge TimeGauge;

    public GameObject Player;

    public bool bTimeMove = true;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        TimeGauge = Managers.GetComponent<TimeGauge>();

        TimeGauge.RecordStart();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && ShopUI.activeSelf){
            ShopUI.SetActive(false);
            StopTime();
        }
    }

    public void GameClear()
    {

    }

    public void GameOver()
    {

    }

    public void GoShop()
    {
        ShopUI.SetActive(true);
        StopTime();
    }

    void StopTime()
    {
        bTimeMove = !bTimeMove;
        Time.timeScale = Convert.ToInt32(bTimeMove);
    }
}
