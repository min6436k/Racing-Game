 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseItem : MonoBehaviour
{
    public EnumTypes.Item ItemType;
    public virtual void GetItem()
    {
        GameManager.instance.Managers.GetComponentInChildren<InGameUIManager>().SetLastItemName(gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
        }
    }
}
public class ItemManager : MonoBehaviour
{
    public Transform WayPoints;

    public List<GameObject> Items = new List<GameObject>();
    public List<GameObject> Coins = new List<GameObject>();

    private List<GameObject> SpawnItems = new List<GameObject>();

    public void Spawn()
    {
        foreach(GameObject i in SpawnItems)
        {
            Destroy(i);
        }

        foreach (Transform t in WayPoints.transform)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-8, 8), 100f, Random.Range(-8, 8));

            if(Random.Range(0, 5) == 0) //1/5 확률로 스폰
            {
                int SpawnItemIndex = Random.Range(0, Items.Count + 1);

                GameObject instance;

                if (Random.Range(0, Items.Count + 1) == Items.Count) //코인은 확률상 전부 합쳐서 다른 아이템 하나와 동일
                {
                    instance = Instantiate(Coins[Random.Range(0, Coins.Count)], t.position + SpawnPos, Quaternion.identity);
                }
                else
                {
                    instance = Instantiate(Items[Random.Range(0, Items.Count)], t.position + SpawnPos, Quaternion.identity);
                }

                RaycastHit hit;
                if(Physics.Raycast(instance.transform.position,Vector3.down, out hit))
                {
                    instance.transform.position = hit.point + Vector3.up;
                }
                else
                {
                    Destroy(instance);
                    continue;
                }

                SpawnItems.Add(instance);
            }
        }
    }
}
