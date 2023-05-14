using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FinalResult : MonoBehaviour
{
    public ItemsManager itemsManager;
    public int[] numberOfItemSent;

    public void Start()
    {
        var listOfItems = itemsManager.items;
        numberOfItemSent = new int[listOfItems.Count];
    }

    public void IncrementItemSent(SO_Item sO_Item)
    {
        var indexOfSO = GetIndexFrom(sO_Item);
        numberOfItemSent[indexOfSO]++;
    }

    public int GetIndexFrom(SO_Item sO_Item)
    {
        var listOfItems = itemsManager.items;
        var index = listOfItems.FindIndex(x => x.Equals(sO_Item));
        Debug.Log("index " + index);
        return index;
    }
}
