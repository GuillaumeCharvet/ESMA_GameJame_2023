using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPosition {TapisRoulant_G, TapisRoulant_D, Main, Etabli, Stockage };
public class CS_Item : MonoBehaviour
{
    public SO_Item SO_Item;
    public ItemPosition currentItemPosition;
    public ItemsManager itemsManager;

    private float tapisRoulantPos = 0f;
    private int currentTapisRoulantIndex = 0;

    public void Move()
    {
        switch (currentItemPosition)
        {
            case ItemPosition.TapisRoulant_G:

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
                        transform.position = Vector3.MoveTowards(itemsManager.posTapisGauche[currentTapisRoulantIndex].position, itemsManager.posTapisGauche[currentTapisRoulantIndex + 1].position, tapisRoulantPos);
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
                        transform.position = Vector3.MoveTowards(itemsManager.posTapisDroite[currentTapisRoulantIndex].position, itemsManager.posTapisDroite[currentTapisRoulantIndex + 1].position, tapisRoulantPos);
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



                break;

            case ItemPosition.Etabli:

                break;

            case ItemPosition.Stockage:

                break;

            default:
                break;
        }
    }
}
