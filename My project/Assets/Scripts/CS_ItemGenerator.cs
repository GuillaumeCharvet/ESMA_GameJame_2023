using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cote {Gauche, Droite };
public class CS_ItemGenerator : MonoBehaviour
{
    private float itemSpawnFrequency = 2f;
    private float itemSpawnFrequencyRandomness = 1f;

    private float timeG = 4f;
    private float timeD = 5f;

    [SerializeField] private ItemsManager itemsManager;

    void Update()
    {
        timeG -= Time.deltaTime;
        if (timeG <= 0f)
        {
            timeG = itemSpawnFrequency + Random.Range(0f, itemSpawnFrequencyRandomness);
            SpawnItem(Cote.Gauche);
        }

        timeD -= Time.deltaTime;
        if (timeD <= 0f)
        {
            timeD = itemSpawnFrequency + Random.Range(0f, itemSpawnFrequencyRandomness);
            SpawnItem(Cote.Droite);
        }
    }

    public void SpawnItem(Cote cote)
    {
        var soItem = itemsManager.GetRandomItem(1);
        
        var item = Instantiate(soItem.prefab);
        item.tag = "Item";
        item.layer =  LayerMask.NameToLayer("Item");

        var colliderAbsent = (item.GetComponent<Collider>() == null);
        if (colliderAbsent)
        {
            item.AddComponent<MeshCollider>();
        }

        var csItem = item.AddComponent<CS_Item>();
        itemsManager.existingItems.Add(csItem);
        csItem.SO_Item = soItem;
        csItem.itemsManager = itemsManager;
        if (cote == Cote.Gauche) csItem.currentItemPosition = ItemPosition.TapisRoulant_G;
        else csItem.currentItemPosition = ItemPosition.TapisRoulant_D;
    }
}
