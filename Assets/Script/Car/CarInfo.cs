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
            BestTime = GameManager.instance.TimeGauge.PlayTime; //ù ��� ����
            _lastTime = BestTime; //���� �� ���� �ð� ���
        }
        else
        {
            BestTime = Mathf.Min(BestTime, GameManager.instance.TimeGauge.PlayTime - _lastTime); //�ְ� ��� ����
            _lastTime = GameManager.instance.TimeGauge.PlayTime; //���� �� ���� �ð� ���
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
