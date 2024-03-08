using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    private Stack<GameObject> OnUIList = new Stack<GameObject>();

    public GameObject TitleUI;

    public GameObject[] StageRankingUI;

    private void Start()
    {
        OnUIList.Push(TitleUI);
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
}
