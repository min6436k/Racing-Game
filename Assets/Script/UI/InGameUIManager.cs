using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{

    public TextMeshProUGUI testtmp;
    private string testtext;
    private CarInfo carinfo;

    private string _lastGetItemName = "";


    public TextMeshProUGUI WinTMP;
    public TextMeshProUGUI CoinTMP;
    public TextMeshProUGUI NextTMP;
    public GameObject GameEndUI;

    public void SetLastItemName(string name)
    {
        _lastGetItemName = name.Substring(0,name.Length-7);
    }

    void Update()
    {
        carinfo = GameManager.instance.Player.GetComponent<CarInfo>();
        //임시 코드
        testtext = "Speed(Km/h) : " + carinfo.KmperHourSpeed.ToString("0.00");
        testtext += "\nLabCount : " + carinfo.Lab;
        testtext += "\nBestTime : " + carinfo.BestTime.ToString("0.00");
        testtext += "\nLastGetItemName : " + _lastGetItemName;

        testtmp.text = testtext;
    }

    public void EndUIOpen(bool Win)
    {
        if (Win == true)
        {
            int stage = GameInstance.instance.CurrentStage;

            GameEndUI.SetActive(true);
            if (stage == 3)
            {
                CoinTMP.text = "All Stage Clear";
                NextTMP.text = "Go to the city of hope";
            }
            else CoinTMP.text = (stage * 5000000).ToString();
        }
        else
        {
            WinTMP.text = "Loss";
            CoinTMP.text = "You Loss Racing";
            NextTMP.text = "Try Again";
        }


    }
}
