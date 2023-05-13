using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Television : MonoBehaviour
{
    private GameObject _emplacement1;
    private GameObject _emplacement2;
    private SO_Item _itemBuild;
    private SO_Item _matPremiere1;
    private SO_Item _matPremiere2;
    [SerializeField] private ItemsManager ItemsManager;
    private float _cdNewItem;

    private void Start()
    {
        _cdNewItem = 20f;
    }
    private void Update()
    {

        _cdNewItem -= Time.deltaTime;

        if(_cdNewItem < 0)
        {
            int Index;
            var list = ItemsManager.tierItems[2];
            _itemBuild = list[Random.Range(0, list.Count)];

            Index = Random.Range(0, ItemsManager.tierItems[].lenght);

        }
    }
}
