using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject Managers;

    public GameObject ShopUI;

    [HideInInspector]
    public TimeGauge TimeGauge;

    public GameObject Player;

    public GameObject NPC_CarPrefab;

    public bool bTimeMove = true;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        TimeGauge = Managers.GetComponent<TimeGauge>();

        TimeGauge.RecordStart();
        SpawnItem();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && ShopUI.activeSelf)
        {
            ShopUI.SetActive(false);
            StopTime();
        }

        Cheat();
    }

    public void GameClear()
    {

        Player.GetComponent<CarController>().IsPlayer = false;

        int stageindex = GameInstance.instance.CurrentStage;

        GameInstance.instance.Stages[stageindex - 1].Cleared = true;

        if (GameInstance.instance.Stages[stageindex - 1].BestTime < TimeGauge.ClearTime)
            GameInstance.instance.Stages[stageindex - 1].BestTime = TimeGauge.ClearTime;

        GameInstance.instance.CurrentClearTimes[stageindex - 1] = TimeGauge.ClearTime;

        if(stageindex == 3)
        {
            GameInstance.instance.AddRanking();
        }
        else
        {
            GameInstance.instance.Coin += 5000000*stageindex;
        }

        GetComponentInChildren<InGameUIManager>().EndUIOpen(true);

        Invoke("LoadMainScene", 5f);
    }

    public void GameOver()
    {
        Player.GetComponent<CarController>().IsPlayer = false;

        GetComponentInChildren<InGameUIManager>().EndUIOpen(false);

        Invoke("ReLoadStage", 5f);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    void ReLoadStage()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void CountDown(TextMeshProUGUI t)
    {
        int Count = int.Parse(t.text);
        if (Count == 1)
        {
            Destroy(t.gameObject);
            GameObject.FindGameObjectWithTag("FinishLine").GetComponent<BoxCollider>().isTrigger = true;

        }

        t.text = (Count-1).ToString();
    }

    public void GoShop()
    {
        ShopUI.SetActive(true);
        StopTime();
    }

    public void SpawnItem()
    {
        GetComponentInChildren<ItemManager>().Spawn();
    }

    void StopTime()
    {
        bTimeMove = !bTimeMove;
        Time.timeScale = Convert.ToInt32(bTimeMove);
    }

    public void SpawnNPC(Vector3 EndPoint)
    {
        if (UnityEngine.Random.Range(0, 10) == 0) //��������Ʈ�� ���� ������ 10�� Ȯ����
        {
            NonPlayCar instance = Instantiate(NPC_CarPrefab).GetComponent<NonPlayCar>();
            instance.EndPoint = EndPoint;
        }
    }


    #region ġƮ

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1)) //������ ���� �� ���� ȹ��
        {

        }

        //���� ���� ġƮ�� Gameinstance��

        if (Input.GetKeyDown(KeyCode.F3)) //�������� �����
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.F4) && GameInstance.instance.CurrentStage < 3) //�������� �̵�
        {
            TimeGauge.RecordEnd();
            GameClear();

            GameInstance.instance.CurrentStage++;
            SceneManager.LoadSceneAsync("Stage"+ GameInstance.instance.CurrentStage);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //���� ����
        {
            StopTime();
        }
    }

    #endregion
}
