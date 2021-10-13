using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InGameScene에 Attach 되어 있는 컴포넌트로 스도쿠 전반을 제어한다.
/// </summary>
public class MatrixControl : MonoBehaviour
{
    public GameObject prefabNumberItem;     // 메인 패널에서 사용될 아이템 원형 프리팹.
    
    public Transform createParent_portrait; // 가로용, 세로용이 필요하다.
    public GridLayoutGroup grid_portrait;
    public InGame_BaseNumber baseNumber_portrait;   // 가로용 하단 숫자 관리 스크립트

    private Question currentQuestion;   // 현재 문제
    private NumberItem[,] itemList;     // 생성한 메인 패널의 아이템들 (인덱스는 좌표)
    private System.Numerics.Vector2 selectedPosition;   // 현재 선택된 셀의 좌표

    /// <summary>
    /// 기본판넬에 NumberItem 프리팹 생성
    /// </summary>
    /// <param name="_type"></param>
    public void CreateMatrix(Question qData)
    {
        currentQuestion = qData;

        #region 메인 패널 생성
        itemList = new NumberItem[qData.length, qData.length];

        for (int i = 0; i < qData.length; i++)
        {
            for (int j = 0; j < qData.length; j++)
            {
                GameObject _instance = GameObject.Instantiate(prefabNumberItem, createParent_portrait) as GameObject;
                NumberItem _item = _instance.GetComponent<NumberItem>();

                int _number = qData.q[i, j];

                // TODO
                // 스도쿠 크기가 추가되면 이쪽도 수정되어야 한다.
                bool _isLeft = j == 0 || j == 3 || j == 6;      // 9*9 베이스로 일단 하드코딩
                bool _isRight = j == 2 || j == 5 || j == qData.length - 1;
                bool _isTop = i == 0 || i == 3 || i == 6;
                bool _isBottom = i == 2 || i == 5 || i == qData.length - 1;

                // area넘버
                // 이거 구하는 수학적 공식이 있을것 같은데...
                // 이리저리 짱돌 굴려봤는데 안나와서 하드코딩 -_-
                int areaNumber = 0;
                if ((-1 < i && i < 3) && (-1 < j && j < 3)) // i == 0~2 && j == 0~2 >> 1
                    areaNumber = 1;
                else if ((-1 < i && i < 3) && (2 < j && j < 6)) // i == 0~2 && j == 3~5 >> 2
                    areaNumber = 2;
                else if ((-1 < i && i < 3) && (5 < j && j < 9)) // i == 0~2 && j == 6~8 >> 3
                    areaNumber = 3;
                else if ((2 < i && i < 6) && (-1 < j && j < 3)) // i == 3~5 && j == 0~2 >> 4
                    areaNumber = 4;
                else if ((2 < i && i < 6) && (2 < j && j < 6)) // i == 3~5 && j == 3~5 >> 5
                    areaNumber = 5;
                else if ((2 < i && i < 6) && (5 < j && j < 9)) // i == 3~5 && j == 6~8 >> 6
                    areaNumber = 6;
                else if ((5 < i && i < 9) && (-1 < j && j < 3)) // i == 6~8 && j == 0~2 >> 7
                    areaNumber = 7;
                else if ((5 < i && i < 9) && (2 < j && j < 6)) // i == 6~8 && j == 3~5 >> 8
                    areaNumber = 8;
                else if ((5 < i && i < 9) && (5 < j && j < 9)) // i == 6~8 && j == 6~8 >> 9
                    areaNumber = 9;

                _item.SetInit(_number, areaNumber, new System.Numerics.Vector2(i, j), _isLeft, _isRight, _isTop, _isBottom);
                _item.onClickEvent += SelectMainItem;

                itemList[i, j] = _item;
            }
        }
        #endregion

        #region 하단 숫자 관리패널 생성 및 이벤트 연결
        baseNumber_portrait.CreateItem(SelectBaseItem);
        #endregion
    }

    /// <summary>
    /// 인게임 메인 패널에서 아이템이 클릭될때.
    /// </summary>
    /// <param name="pos">클릭된 현재 아이템의 좌표</param>
    private void SelectMainItem(System.Numerics.Vector2 pos)
    {
        selectedPosition = pos;

        // 현재 선택된 좌표 얻기
        int _x = (int)selectedPosition.X;
        int _y = (int)selectedPosition.Y;

        // 선택된 셀의 아이템 객체 얻기
        NumberItem selectedItem = itemList[_x, _y];
        
        // 선택된 Area 번호.
        int selectedArea = selectedItem.GetAreaNumber;

        // 선택된 셀의 번호 (0 혹은 음수라면 빈 셀)
        int selectedNumber = selectedItem.GetNumber;

        // 세로 혹은 가로의 길이
        int length = currentQuestion.length;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                MAIN_CELL_STATE _state = MAIN_CELL_STATE.INVALID;

                int targetAreaNumber = itemList[i, j].GetAreaNumber;
                int targetNumber = itemList[i, j].GetNumber;

                // Debug.Log(string.Format("x:{0}, y:{1}, selectedNumber:{2}, selectedArea:{3}, targetAreaNumber:{4}, targetNumber:{5}", i,j, selectedNumber, selectedArea, targetAreaNumber, targetNumber));

                // 우선권
                // 선택된 셀 > 선택된 동일한 숫자 > 같은 행 또는 열 또는 동일 area > 흰색처리
                if (i == _x && j == _y)
                    _state = MAIN_CELL_STATE.SELECTED;
                else if (selectedNumber > 0 && selectedNumber == targetNumber)
                    _state = MAIN_CELL_STATE.SAME_NUMBER;
                else if (i == _x || j == _y || targetAreaNumber == selectedArea)
                    _state = MAIN_CELL_STATE.ALIGNMENT;

                itemList[i, j].SetState(_state);
            }
        }
    }

    /// <summary>
    /// 인게임 하단 숫자 클릭시
    /// </summary>
    /// <param name="value"></param>
    private void SelectBaseItem(int value)
    {
        // 현재 선택된 좌표 얻기
        int _x = (int)selectedPosition.X;
        int _y = (int)selectedPosition.Y;

        // 선택된 셀의 아이템 객체 얻기
        NumberItem selectedItem = itemList[_x, _y];

        selectedItem.SetData(value);
    }
}
