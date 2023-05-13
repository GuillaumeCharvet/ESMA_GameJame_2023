using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Item", order = 1)]
public class SO_Item : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public int itemTier;
    public AudioClip itemSound;
    public bool authorized;
    public SO_Item enfant1, enfant2;
}
