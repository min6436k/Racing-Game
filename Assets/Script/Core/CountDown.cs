using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    void countDown()
    {
        GameManager.instance.CountDown(GetComponent<TextMeshProUGUI>());
    }
}
