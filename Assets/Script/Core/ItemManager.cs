 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
