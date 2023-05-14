using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Television : MonoBehaviour
{
    [SerializeField] private GameObject _emplacement1;
    [SerializeField] private GameObject _emplacement2;
    [SerializeField] private ItemsManager ItemsManager;
    [SerializeField] private float _cdSwitchItem;
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
        var list = ItemsManager.tierItems[1];
        _itemBuild = list[Random.Range(0, list.Count)];

        if (!_itemBuild.authorized)
        {
            NewItemCraft();
        }
        else
        {
            if (_item1 != null)
                Destroy(_item1);
            if (_item2 != null)
                Destroy(_item2);
            

            _matPremiere1 = _itemBuild.enfant1;
            _matPremiere2 = _itemBuild.enfant2;

          
            _item1 = Instantiate(_matPremiere1.prefab);
            _item2 = Instantiate(_matPremiere2.prefab);

           
            _item1.transform.position = _emplacement1.transform.position;
            _item2.transform.position = _emplacement2.transform.position;

          
            _item1.transform.localScale = _emplacement1.transform.localScale;
            _item2.transform.localScale = _emplacement2.transform.localScale;

          
            _item1.transform.rotation = _emplacement1.transform.rotation;
            _item2.transform.rotation = _emplacement2.transform.rotation;

            //_item1.GetComponent<Renderer>().shadowCastingMode;
            _cdNewItem = _cdSwitchItem;
        }
    }
}

