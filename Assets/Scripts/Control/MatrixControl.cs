using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// InGameScene에 Attach 되어 있는 컴포넌트로 스도쿠 전반을 제어한다.
/// </summary>
public class MatrixControl : MonoBehaviour
{
    public GameObject prefabNumberItem;     // 메인 패널에서 사용될 아이템 원형 프리팹.
    public Transform createParent_portrait; // 가로용, 세로용이 필요하다.

    public InGame_BaseNumber baseNumber_portrait;   // 가로용 하단 숫자 관리 스크립트

    /// <summary>
    /// 기본판넬에 NumberItem 프리팹 생성
    /// </summary>
    /// <param name="_type"></param>
    public void CreateMatrix(Question qData)
    {
        #region 메인 패널 생성
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

                _item.SetInit(_number, new System.Numerics.Vector2(i, j), _isLeft, _isRight, _isTop, _isBottom);
                _item.onClickEvent += SelectMainItem;
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
        Debug.Log(pos.X + " / " + pos.Y);
    }

    /// <summary>
    /// 인게임 하단 숫자 클릭시
    /// </summary>
    /// <param name="value"></param>
    private void SelectBaseItem(int value)
    {
        Debug.Log(value);
    }
}
