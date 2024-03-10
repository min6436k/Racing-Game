using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    private Stack<GameObject> OnUIList = new Stack<GameObject>();

    public GameObject TitleUI;

    public GameObject[] StageRankingUI;

    public GameObject[] ClearPanels;

    public TextMeshProUGUI RankingTMP;

    private void Start()
    {
        OnUIList.Push(TitleUI);

        ClearState();
        UpdateRanking();
    }
    public void OpenUI(GameObject TargetUI)
    {
        TargetUI.gameObject.SetActive(true);
        OnUIList.Peek().SetActive(false);
        OnUIList.Push(TargetUI);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && OnUIList.Count > 1)
        {
            OnUIList.Pop().SetActive(false);
            OnUIList.Peek().SetActive(true);
        }
    }

    void ClearState()
    {
        for (int i = 0; i < ClearPanels.Length; i++)
        {
            if (GameInstance.instance.Stages[i].Cleared)
            {
                ClearPanels[i].SetActive(true);
            }
            else
            {
                for (int j = i + 1; j < ClearPanels.Length; j++)
                {
                    ClearPanels[j].SetActive(true);
                }
            }
        }
    }

    void UpdateRanking()
    {
        string[] temp = { "1st : ", "2nd : ", "3rd : ", "4th : ", "5th : " };

        for(int i = 0;i < GameInstance.instance.TotalRanking.Count; i++)
        {
            temp[i] += GameInstance.instance.TotalRanking[i];
        }
        RankingTMP.text = string.Join("\n",temp);
    }

    public void GoToStage(int SceneNum)
    {
        SceneManager.LoadSceneAsync("Stage"+SceneNum);
        GameInstance.instance.CurrentStage = SceneNum;
    }
}
