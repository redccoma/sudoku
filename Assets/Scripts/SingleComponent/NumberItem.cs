using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class NumberItem : MonoBehaviour
{
    public Text mText;

    public GameObject leftLine;
    public GameObject rightLine;
    public GameObject topLine;
    public GameObject bottomLine;

    private int myNumber;

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
    public void SetInit(int number, bool isLeft, bool isRight, bool isTop, bool isBottom)
    {
        _isQuestion = number <= 0;

        if (number <= 0)
            mText.text = string.Empty;
        else
            mText.text = number.ToString();

        myNumber = number;

        leftLine.SetActive(isLeft);
        rightLine.SetActive(isRight);
        topLine.SetActive(isTop);
        bottomLine.SetActive(isBottom);
    }

    /// <summary>
    /// 유저 입력시
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {

    }

    public void OnClick()
    {
        Debug.Log(myNumber);
    }
}
