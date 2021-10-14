using System;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임 상단 메인 문제풀이에서 사용되는 Item 전용 스크립트
/// </summary>
public class NumberItem : MonoBehaviour
{
    public Text mText;
    public Image selectedColor;

    public GameObject leftLine;
    public GameObject rightLine;
    public GameObject topLine;
    public GameObject bottomLine;

    public Action<System.Numerics.Vector2> onClickEvent;   // 클릭시 처리할 이벤트

    private int myNumber;   // 현재 숫자
    private int myArea;     // 에어리어 (사각 영역 넘버링)
    private System.Numerics.Vector2 myPosition; // 내 좌표
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
    /// 지정된 Area 번호 얻기
    /// </summary>
    public int GetAreaNumber
    {
        get
        {
            return myArea;
        }
    }

    /// <summary>
    /// 현재 아이템의 숫자 얻기
    /// </summary>

    public int GetNumber
    {
        get
        {
            return myNumber;
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
    public void SetInit(int number, int areaNumber, System.Numerics.Vector2 position, bool isLeft, bool isRight, bool isTop, bool isBottom)
    {
        _isQuestion = number <= 0;

        if (number <= 0)
            mText.text = string.Empty;
        else
            mText.text = number.ToString();

        myNumber = number;
        myArea = areaNumber;
        myPosition = position;

        leftLine.SetActive(isLeft);
        rightLine.SetActive(isRight);
        topLine.SetActive(isTop);
        bottomLine.SetActive(isBottom);
    }

    /// <summary>
    /// 셀 숫자 수정
    /// </summary>
    /// <param name="number">반영할 숫자 (0 또는 음수인 경우 값 초기화)</param>
    /// <param name="isMatched">정답과 맞는가?</param>
    public void SetData(int number, bool isMatched)
    {
        if (number > 0)
        {
            myNumber = number;
            mText.text = number.ToString();
        }
        else
        {
            myNumber = 0;
            mText.text = string.Empty;
        }

        if (isMatched)
            mText.color = Color.black;
        else
            mText.color = Color.red;
    }

    /// <summary>
    /// 셀 상태 수정
    /// </summary>
    /// <param name="_state">셀의 컬러 변경시</param>
    public void SetState(MAIN_CELL_STATE _state)
    {
        switch (_state)
        {
            case MAIN_CELL_STATE.SELECTED:
                selectedColor.color = new Color(187 / 255f, 224 / 255f, 253 / 255f, 1f);
                break;
            case MAIN_CELL_STATE.SAME_NUMBER:
                selectedColor.color = new Color(199 / 255f, 207 / 255f, 220 / 255f, 1f);
                break;
            case MAIN_CELL_STATE.ALIGNMENT:
                selectedColor.color = new Color(228 / 255f, 231 / 255f, 238 / 255f, 1f);
                break;
            case MAIN_CELL_STATE.INVALID:
                selectedColor.color = Color.white;
                break;
        }
    }

    public void OnClick()
    {
        onClickEvent?.Invoke(myPosition);
    }
}
