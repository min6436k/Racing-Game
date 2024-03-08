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
    
    public void SetLastItemName(string name)
    {
        _lastGetItemName = name;
    }

    void Update()
    {
        carinfo = GameManager.instance.Player.GetComponent<CarInfo>();
        //임시 코드
        testtext = "Speed(Km/h) : " + carinfo.KmperHourSpeed.ToString();
        testtext += "\nLabCount : " + carinfo.Lab;
        testtext += "\nBestTime : " + carinfo.BestTime;
        testtext += "\nLastGetItemName : " + _lastGetItemName;

        testtmp.text = testtext;
    }
}
