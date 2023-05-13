using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPosition {TapisRoulant_G, TapisRoulant_D, Main, Trash, Depot, Etabli_G, Etabli_D, Stockage_1, Stockage_2, Stockage_3, Stockage_4, Stockage_5 };
public class CS_Item : MonoBehaviour
{
    public SO_Item SO_Item;
    public ItemPosition currentItemPosition;
    public ItemsManager itemsManager;

    public float valueFromTapisToHand = 0f;
    public Vector3 lastPositionOnTapis;
    public Vector3 lastPositionInHand;

    private float tapisRoulantPos = 0f;
    private int currentTapisRoulantIndex = 0;

    private float trashPos = 0f;
    private int currentTrashIndex = 0;

    public float depotPos = 0f;
    public int currentDepotIndex = 0;

    public void Move()
    {
        switch (currentItemPosition)
        {
            case ItemPosition.TapisRoulant_G:

                transform.localScale = Mathf.LerpUnclamped(1f, itemsManager.sizeOfItemInHand, itemsManager.rescaleProfile.Evaluate(valueFromTapisToHand)) * Vector3.one;

                if (currentTapisRoulantIndex < itemsManager.posTapisGauche.Count - 1)
                {
                    tapisRoulantPos += itemsManager.tapisSpeed * Time.deltaTime;
                    if (tapisRoulantPos > Vector3.Distance(itemsManager.posTapisGauche[currentTapisRoulantIndex].position ,itemsManager.posTapisGauche[currentTapisRoulantIndex + 1].position))
                    {
                        currentTapisRoulantIndex++;
                        tapisRoulantPos = 0f;
                    }
                    if (currentTapisRoulantIndex < itemsManager.posTapisGauche.Count - 1)
                    {
                        var targetPos = Vector3.MoveTowards(itemsManager.posTapisGauche[currentTapisRoulantIndex].position, itemsManager.posTapisGauche[currentTapisRoulantIndex + 1].position, tapisRoulantPos);

                        transform.position = Vector3.Lerp(targetPos, lastPositionInHand, valueFromTapisToHand);
                    }
                    else
                    {
                        itemsManager.existingItems.Remove(this);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    itemsManager.existingItems.Remove(this);
                    Destroy(gameObject);
                }
                break;

            case ItemPosition.TapisRoulant_D:

                transform.localScale = Mathf.Lerp(1f, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                if (currentTapisRoulantIndex < itemsManager.posTapisDroite.Count - 1)
                {
                    tapisRoulantPos += itemsManager.tapisSpeed * Time.deltaTime;
                    if (tapisRoulantPos > Vector3.Distance(itemsManager.posTapisDroite[currentTapisRoulantIndex].position, itemsManager.posTapisDroite[currentTapisRoulantIndex + 1].position))
                    {
                        currentTapisRoulantIndex++;
                        tapisRoulantPos = 0f;
                    }
                    if (currentTapisRoulantIndex < itemsManager.posTapisDroite.Count - 1)
                    {
                        var targetPos = Vector3.MoveTowards(itemsManager.posTapisDroite[currentTapisRoulantIndex].position, itemsManager.posTapisDroite[currentTapisRoulantIndex + 1].position, tapisRoulantPos);

                        transform.position = Vector3.Lerp(targetPos, lastPositionInHand, valueFromTapisToHand);
                    }
                    else
                    {
                        itemsManager.existingItems.Remove(this);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    itemsManager.existingItems.Remove(this);
                    Destroy(gameObject);
                }
                break;

            case ItemPosition.Main:

                var screenPos = Input.mousePosition;
                screenPos.z = itemsManager.cam.nearClipPlane + itemsManager.handDistanceToCam;

                transform.position = Vector3.Lerp(lastPositionOnTapis, itemsManager.cam.ScreenToWorldPoint(screenPos), valueFromTapisToHand);
                transform.localScale = Mathf.Lerp(1f, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                break;

            case ItemPosition.Trash:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInTrash, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                if (currentTrashIndex < itemsManager.posTrash.Count - 1)
                {
                    trashPos += itemsManager.trashSpeed * Time.deltaTime;
                    if (trashPos > Vector3.Distance(itemsManager.posTrash[currentTrashIndex].position, itemsManager.posTrash[currentTrashIndex + 1].position))
                    {
                        currentTrashIndex++;
                        trashPos = 0f;
                    }
                    if (currentTrashIndex < itemsManager.posTrash.Count - 1)
                    {
                        var targetPos = Vector3.MoveTowards(itemsManager.posTrash[currentTrashIndex].position, itemsManager.posTrash[currentTrashIndex + 1].position, trashPos);

                        transform.position = Vector3.Lerp(targetPos, lastPositionInHand, valueFromTapisToHand);
                    }
                    else
                    {
                        itemsManager.existingItems.Remove(this);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    itemsManager.existingItems.Remove(this);
                    Destroy(gameObject);
                }

                break;

            case ItemPosition.Depot:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInDepot, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                if (currentDepotIndex < itemsManager.posDepot.Count - 1)
                {
                    depotPos += itemsManager.depotSpeed * Time.deltaTime;
                    if (depotPos > Vector3.Distance(itemsManager.posDepot[currentDepotIndex].position, itemsManager.posDepot[currentDepotIndex + 1].position))
                    {
                        currentDepotIndex++;
                        depotPos = 0f;
                    }
                    if (currentDepotIndex < itemsManager.posDepot.Count - 1)
                    {
                        var targetPos = Vector3.MoveTowards(itemsManager.posDepot[currentDepotIndex].position, itemsManager.posDepot[currentDepotIndex + 1].position, depotPos);

                        transform.position = Vector3.Lerp(targetPos, lastPositionInHand, valueFromTapisToHand);
                    }
                    else
                    {
                        itemsManager.existingItems.Remove(this);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    itemsManager.existingItems.Remove(this);
                    Destroy(gameObject);
                }

                break;

            case ItemPosition.Etabli_G:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInEtabli, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posEtabli_G.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Etabli_D:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInEtabli, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posEtabli_D.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Stockage_1:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInStockage, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posStockage_1.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Stockage_2:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInStockage, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posStockage_2.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Stockage_3:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInStockage, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posStockage_3.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Stockage_4:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInStockage, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posStockage_4.position, lastPositionInHand, valueFromTapisToHand);

                break;

            case ItemPosition.Stockage_5:

                transform.localScale = Mathf.Lerp(itemsManager.sizeOfItemInStockage, itemsManager.sizeOfItemInHand, valueFromTapisToHand) * Vector3.one;

                transform.position = Vector3.Lerp(itemsManager.posStockage_5.position, lastPositionInHand, valueFromTapisToHand);

                break;
            default:
                break;
        }
    }
}
