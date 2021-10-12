using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene에 Attach 되어 있는 컴포넌트로 스도쿠 전반을 제어한다.
/// </summary>
public class MatrixControl : MonoBehaviour
{
    public GameObject prefabNumberItem;

    public Transform createParent_portrait; // 가로용, 세로용이 필요하다.

    /// <summary>
    /// 기본판넬에 NumberItem 프리팹 생성
    /// </summary>
    /// <param name="_type"></param>
    public void CreateMatrix(Question qData)
    {
        for (int i = 0; i < qData.length; i++)
        {
            for (int j = 0; j < qData.length; j++)
            {
                GameObject _instance = GameObject.Instantiate(prefabNumberItem, createParent_portrait) as GameObject;
                NumberItem _item = _instance.GetComponent<NumberItem>();

                int _number = qData.q[i, j];
                bool _isLeft = j == 0 || j == 3 || j == 6;
                bool _isRight = j == 2 || j == 5 || j == qData.length - 1;
                bool _isTop = i == 0 || i == 3 || i == 6;
                bool _isBottom = i == 2 || i == 5 || i == qData.length - 1;

                _item.SetInit(_number, _isLeft, _isRight, _isTop, _isBottom);
            }
        }
    }
}
