using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Television : MonoBehaviour
{
    [SerializeField] private GameObject _emplacement1;
    [SerializeField] private GameObject _emplacement2;
    [SerializeField] private ItemsManager ItemsManager;
    private GameObject _item1;
    private GameObject _item2;
    private SO_Item _matPremiere1;
    private SO_Item _matPremiere2;

    [Header("Info Debug")]
    [SerializeField] private SO_Item _itemBuild;
    [SerializeField] private float _cdNewItem;

    private void Start()
    {
        _cdNewItem = 0f;
    }
    private void Update()
    {
        _cdNewItem -= Time.deltaTime;

        if(_cdNewItem < 0)
        {
            NewItemCraft();
        }
    }
    private void NewItemCraft()
    {
        if(_item1 != null)
            Destroy(_item1);
        if (_item2 != null)
            Destroy(_item2);

        var list = ItemsManager.tierItems[2];
        _itemBuild = list[Random.Range(0, list.Count)];

        _matPremiere1 = _itemBuild.enfant1;
        _matPremiere2 = _itemBuild.enfant2;

        _item1 = Instantiate(_matPremiere1.prefab);
        _item2 = Instantiate(_matPremiere2.prefab);
        _item1.transform.position = _emplacement1.transform.position;
        _item2.transform.position = _emplacement2.transform.position;

        _cdNewItem = 20f;
    }
}

