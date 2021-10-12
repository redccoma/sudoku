using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_BaseNumber : MonoBehaviour
{
    public GameObject prefab;
    public Transform prefabParent;

    public void Awake()
    {
        CreateItem();
    }

    public void CreateItem()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject _instance = GameObject.Instantiate(prefab, prefabParent) as GameObject;
            BaseNumberItem _item = _instance.GetComponent<BaseNumberItem>();

            _item.SetData(i + 1);
            _item.onClickEvent += OnClickEvent;
        }
    }

    public void OnClickEvent(int number)
    {
        Debug.Log(number);
    }
}