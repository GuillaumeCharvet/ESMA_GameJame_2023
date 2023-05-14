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
    public AnimationCurve rescaleProfileTapis;
    public AnimationCurve rescaleProfileMain;
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
    public CS_Item itemInEtabliG, itemInEtabliD;
    public Transform posEtabli_G, posEtabli_D;
    public float etabliSpeed = 0.45f;
    public float sizeOfItemInEtabli = 0.8f;

    [Header("Stockage")]
    public CS_Item itemInStockage_1, itemInStockage_2, itemInStockage_3, itemInStockage_4, itemInStockage_5;
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

                    if (item.currentItemPosition == ItemPosition.Etabli_G) itemInEtabliG = null;
                    else if (item.currentItemPosition == ItemPosition.Etabli_D) itemInEtabliD = null;
                    else if (item.currentItemPosition == ItemPosition.Stockage_1) itemInStockage_1 = null;
                    else if (item.currentItemPosition == ItemPosition.Stockage_2) itemInStockage_2 = null;
                    else if (item.currentItemPosition == ItemPosition.Stockage_3) itemInStockage_3 = null;
                    else if (item.currentItemPosition == ItemPosition.Stockage_4) itemInStockage_4 = null;
                    else if (item.currentItemPosition == ItemPosition.Stockage_5) itemInStockage_5 = null;

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
                    if (itemInEtabliG == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Etabli_G;
                        itemInEtabliG = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Etabli_D"))
                {
                    if (itemInEtabliD == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Etabli_D;
                        itemInEtabliD = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Stockage_1"))
                {
                    if (itemInStockage_1 == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Stockage_1;
                        itemInStockage_1 = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Stockage_2"))
                {
                    if (itemInStockage_2 == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Stockage_2;
                        itemInStockage_2 = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Stockage_3"))
                {
                    if (itemInStockage_3 == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Stockage_3;
                        itemInStockage_3 = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Stockage_4"))
                {
                    if (itemInStockage_4 == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Stockage_4;
                        itemInStockage_4 = itemInHand;
                    }
                    else
                    {
                        itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
                    }
                }
                else if (hit.transform.CompareTag("Stockage_5"))
                {
                    if (itemInStockage_5 == null)
                    {
                        itemInHand.currentItemPosition = ItemPosition.Stockage_5;
                        itemInStockage_5 = itemInHand;
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
            }
            else
            {
                itemInHand.currentItemPosition = coteOfItemInHand == Cote.Gauche ? ItemPosition.TapisRoulant_G : ItemPosition.TapisRoulant_D;
            }

            itemInHand.lastPositionInHand = itemInHand.transform.position;

            //StopCoroutine(IncreaseValue(itemInHand));
            StopAllCoroutines();
            StartCoroutine(DecreaseValue(itemInHand));

            itemInHand = null;
            isItemInHand = false;
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
