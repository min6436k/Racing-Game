using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    public int Lab = 0;

    public float BestTime = 0;
    private float _lastTime = 0;

    public float KmperHourSpeed;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

    }
    public void FinishLine()
    {
        if (Lab == 0)
        {
            BestTime = GameManager.instance.TimeGauge.PlayTime; //첫 기록 설정
            _lastTime = BestTime; //이전 랩 돌파 시간 기록
        }
        else
        {
            BestTime = Mathf.Min(BestTime, GameManager.instance.TimeGauge.PlayTime - _lastTime); //최고 기록 갱신
            _lastTime = GameManager.instance.TimeGauge.PlayTime; //이전 랩 돌파 시간 기록
        }

        Lab++;

        if (GetComponent<CarController>().IsPlayer == true) GameManager.instance.SpawnItem();
        
    }

    private void FixedUpdate()
    {
        KmperHourSpeed = rigid.velocity.magnitude * 3.6f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FinishLine") && GetComponent<CarController>().bPassLastPoint)
        {
            GetComponent<CarController>().bPassLastPoint = false;
            FinishLine();

            if (Lab == 3)
            {
                if (GetComponent<CarController>().IsPlayer)
                {
                    GameManager.instance.TimeGauge.RecordEnd();
                    GameManager.instance.GameClear();
                }
                else GameManager.instance.GameOver();
            }

        }
    }
}
