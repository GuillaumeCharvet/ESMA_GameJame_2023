using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ItemMerger : MonoBehaviour
{
    public ItemsManager itemsManager;

    private List<SO_Item[]> mergeTable = new List<SO_Item[]>();

    public void Start()
    {
        var listItems = itemsManager.items;
        for (int i = 0; i < listItems.Count; i++)
        {
            if (listItems[i].enfant1 != null)
            {
                var tab = new SO_Item[3];
                tab[0] = listItems[i].enfant1;
                tab[1] = listItems[i].enfant2;
                tab[2] = listItems[i];
                mergeTable.Add(tab);
            }
        }
        Debug.Log("taille merge table = " + mergeTable.Count);
    }

    public void PressButton()
    {
        Debug.Log("ATTEMPT MERGING");
        var itemG = itemsManager.itemInEtabliG;
        var itemD = itemsManager.itemInEtabliD;
        var mergeResult = MergeResult(itemG.SO_Item, itemD.SO_Item);
        if (mergeResult != null)
        {
            itemsManager.existingItems.Remove(itemG);
            Destroy(itemsManager.itemInEtabliG.gameObject);
            itemsManager.existingItems.Remove(itemD);
            Destroy(itemsManager.itemInEtabliD.gameObject);

            itemsManager.itemInEtabliG = null;
            itemsManager.itemInEtabliD = null;            

            itemsManager.itemInEtabliG = SpawnItem(mergeResult);
        }
    }

    public SO_Item MergeResult(SO_Item itemG, SO_Item itemD)
    {
        for (int i = 0; i < mergeTable.Count; i++)
        {
            if (itemG.name == mergeTable[i][0].name)
            {
                if (itemD.name == mergeTable[i][1].name)
                {
                    Debug.Log("SHOULD MERGE");
                    return mergeTable[i][2];
                }
            }
            else if (itemG.name == mergeTable[i][1].name)
            {
                if (itemD.name == mergeTable[i][0].name)
                {
                    Debug.Log("SHOULD MERGE");
                    return mergeTable[i][2];
                }
            }
        }
        return null;
    }
    public CS_Item SpawnItem(SO_Item soItem)
    {
        var item = Instantiate(soItem.prefab);
        item.tag = "Item";
        item.layer = LayerMask.NameToLayer("Item");

        var colliderAbsent = (item.GetComponent<Collider>() == null);
        if (colliderAbsent)
        {
            item.AddComponent<MeshCollider>();
        }

        var csItem = item.AddComponent<CS_Item>();
        itemsManager.existingItems.Add(csItem);
        csItem.SO_Item = soItem;
        csItem.itemsManager = itemsManager;
        csItem.currentItemPosition = ItemPosition.Etabli_G;

        return csItem;
    }
}
