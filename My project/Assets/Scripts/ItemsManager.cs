using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemsManager : MonoBehaviour
{
    public List<SO_Item> items = new List<SO_Item>();

    public List<SO_Item> authorizedItems = new List<SO_Item>();
    public List<SO_Item> unauthorizedItems = new List<SO_Item>();

    public List<SO_Item>[] tierItems = new List<SO_Item>[3];

    public List<CS_Item> existingItems = new List<CS_Item>();

    public LayerMask grabLayer, dropLayer;

    [Header ("Tapis")]
    public List<Transform> posTapisGauche, posTapisDroite;
    public float tapisSpeed = 0.45f;

    [Header("Main")]
    public bool isItemInHand = false;
    public Camera cam;
    public CS_Item itemInHand;
    public float handDistanceToCam = 3f;
    public float sizeOfItemInHand = 0.4f;
    public float speedFromTapisToHand = 2f;
    public float speedFromHandToTapis = 4f;
    public Cote coteOfItemInHand;
    public AnimationCurve speedProfile;

    [Header("Trash")]
    public List<Transform> posTrash;
    public float trashSpeed = 0.45f;
    public float sizeOfItemInTrash = 0.6f;

    [Header("Depot")]
    public List<Transform> posDepot;
    public float depotSpeed = 0.45f;
    public float sizeOfItemInDepot = 0.6f;

    [Header("Etabli")]
    public Transform posEtabli_G, posEtabli_D;
    public float etabliSpeed = 0.45f;
    public float sizeOfItemInEtabli = 0.8f;

    [Header("Stockage")]
    public Transform posStockage_1, posStockage_2, posStockage_3, posStockage_4, posStockage_5;
    public float stockageSpeed = 0.45f;
    public float sizeOfItemInStockage = 0.25f;


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
        CheckIfGrabbingItem();

        CheckIfDroppingItem();
    }

    public void LateUpdate()
    {
        foreach (var item in existingItems)
        {
            item.Move();
        }
    }

    public void CheckIfGrabbingItem()
    {
        if (Input.GetMouseButtonDown(0) && !isItemInHand)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, grabLayer))
            {
                if (hit.transform.CompareTag("Item"))
                {
                    Transform objectHit = hit.transform;
                    var item = objectHit.GetComponent<CS_Item>();

                    isItemInHand = true;
                    itemInHand = item;
                    coteOfItemInHand = item.currentItemPosition == ItemPosition.TapisRoulant_G ? Cote.Gauche : Cote.Droite;
                    item.lastPositionOnTapis = objectHit.position;
                    StartCoroutine(IncreaseValue(itemInHand));

                    item.currentItemPosition = ItemPosition.Main;
                }
            }
        }
    }

    public IEnumerator IncreaseValue(CS_Item item)
    {
        while (item.valueFromTapisToHand < 1f)
        {
            item.valueFromTapisToHand += speedFromTapisToHand * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void CheckIfDroppingItem()
    {
        if (Input.GetMouseButtonUp(0) && isItemInHand)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, dropLayer))
            {
                if (hit.transform.CompareTag("Trash"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Trash;
                }
                else if (hit.transform.CompareTag("DropZone"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Depot;
                }
                else if (hit.transform.CompareTag("Etabli_G"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Etabli_G;
                }
                else if (hit.transform.CompareTag("Etabli_D"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Etabli_D;
                }
                else if (hit.transform.CompareTag("Stockage_1"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Stockage_1;
                }
                else if (hit.transform.CompareTag("Stockage_2"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Stockage_2;
                }
                else if (hit.transform.CompareTag("Stockage_3"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Stockage_3;
                }
                else if (hit.transform.CompareTag("Stockage_4"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Stockage_4;
                }
                else if (hit.transform.CompareTag("Stockage_5"))
                {
                    itemInHand.currentItemPosition = ItemPosition.Stockage_5;
                }
                else
                {
                    itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                }
            }
            else
            {
                itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
            }

            itemInHand.lastPositionInHand = itemInHand.transform.position;
            isItemInHand = false;

            //StopCoroutine(IncreaseValue(itemInHand));
            StopAllCoroutines();
            StartCoroutine(DecreaseValue(itemInHand));
        }
    }
    public IEnumerator DecreaseValue(CS_Item item)
    {
        while (item.valueFromTapisToHand > 0f)
        {
            item.valueFromTapisToHand -= speedFromHandToTapis * Time.deltaTime;
            yield return new WaitForEndOfFrame();
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
