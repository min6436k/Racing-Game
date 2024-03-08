using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGauge : MonoBehaviour
{
    private float _startTime = 0;
    private float _endTime = 0;

    public float PlayTime => (Time.time-_startTime);
    public float ClearTime;

    public void RecordStart()
    {
        _startTime = Time.time;
    }

    public void RecordEnd()
    {
        _endTime = Time.time;
        ClearTime = _startTime - _endTime;
    }
}
