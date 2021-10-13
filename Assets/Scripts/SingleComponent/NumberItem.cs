using System;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임 상단 메인 문제풀이에서 사용되는 Item 전용 스크립트
/// </summary>
public class NumberItem : MonoBehaviour
{
    public Text mText;

    public GameObject leftLine;
    public GameObject rightLine;
    public GameObject topLine;
    public GameObject bottomLine;

    public Action<System.Numerics.Vector2> onClickEvent;   // 클릭시 처리할 이벤트

    private int myNumber;
    private System.Numerics.Vector2 myPosition;

    private bool _isQuestion = false;
    /// <summary>
    /// 유저가 입력 가능한 필드인가.
    /// </summary>
    public bool IsQuestion
    {
        get
        {
            return _isQuestion;
        }
    }

    /// <summary>
    /// 최초 설정시
    /// </summary>
    /// <param name="number">1~n, 혹은 0(유저가 입력해야 하는 값)</param>
    /// <param name="isLeft">외곽라인(왼쪽)</param>
    /// <param name="isRight">외곽라인(오른쪽)</param>
    /// <param name="isTop">외곽라인(위)</param>
    /// <param name="isBottom">외곽라인(아래)</param>
    public void SetInit(int number, System.Numerics.Vector2 position, bool isLeft, bool isRight, bool isTop, bool isBottom)
    {
        _isQuestion = number <= 0;

        if (number <= 0)
            mText.text = string.Empty;
        else
            mText.text = number.ToString();

        myNumber = number;
        myPosition = position;

        leftLine.SetActive(isLeft);
        rightLine.SetActive(isRight);
        topLine.SetActive(isTop);
        bottomLine.SetActive(isBottom);
    }

    /// <summary>
    /// 유저 입력시 호출.
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {

    }

    public void OnClick()
    {
        onClickEvent?.Invoke(myPosition);
    }
}
