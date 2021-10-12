using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임 씬 하단의 선택가능한 숫자 제어스크립트.
/// </summary>
public class BaseNumberItem : MonoBehaviour
{
    public Text mText;

    private int myNumber;

    public Action<int> onClickEvent;

    public void SetData(int number)
    {
        myNumber = number;
        mText.text = number.ToString();
    }

    public void OnClick()
    {
        onClickEvent?.Invoke(myNumber);
    }
}
