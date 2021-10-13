using System;

using UnityEngine;

public class InGame_BaseNumber : MonoBehaviour
{
    public GameObject prefab;
    public Transform prefabParent;

    /// <summary>
    /// 하단 아이템 생성
    /// </summary>
    /// <param name="action">하단 숫자 클릭시 수신받을 이벤트</param>
    public void CreateItem(Action<int> action)
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject _instance = GameObject.Instantiate(prefab, prefabParent) as GameObject;
            BaseNumberItem _item = _instance.GetComponent<BaseNumberItem>();

            _item.SetData(i + 1);
            _item.onClickEvent += action;
        }
    }
}