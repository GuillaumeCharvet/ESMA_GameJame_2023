using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public List<SO_Item> items = new List<SO_Item>();

    public List<SO_Item> authorizedItems = new List<SO_Item>();
    public List<SO_Item> unauthorizedItems = new List<SO_Item>();

    public List<SO_Item>[] tierItems = new List<SO_Item>[3];

    public List<CS_Item> existingItems = new List<CS_Item>();

    public List<Transform> posTapisGauche, posTapisDroite;
    public float tapisSpeed = 0.45f;

    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            tierItems[i] = new List<SO_Item>();
        }
        foreach (var item in items)
        {
            if (item.authorized)
            {
                authorizedItems.Add(item);
            }
            else
            {
                unauthorizedItems.Add(item);
            }
            tierItems[item.itemTier - 1].Add(item);

        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    public void LateUpdate()
    {
        foreach (var item in existingItems)
        {
            item.Move();
        }
    }

    public SO_Item GetRandomItem()
    {
        int randomInd = Random.Range(0, items.Count);
        return items[randomInd];
    }

    public SO_Item GetRandomItem(int tier)
    {
        int randomInd = Random.Range(0, tierItems[tier - 1].Count);
        Debug.Log("random index = " + randomInd);
        return tierItems[tier - 1][randomInd];
    }
    public SO_Item GetRandomItem(bool authorized)
    {
        if (authorized)
        {
            int randomInd = Random.Range(0, authorizedItems.Count);
            return authorizedItems[randomInd];
        }
        else
        {
            int randomInd = Random.Range(0, unauthorizedItems.Count);
            return unauthorizedItems[randomInd];
        }
    }
}
