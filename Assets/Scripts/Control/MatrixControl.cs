using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene에 Attach 되어 있는 컴포넌트로 스도쿠 전반을 제어한다.
/// </summary>
public class MatrixControl : MonoBehaviour
{
    public enum MatrixType
    {
        /// <summary>
        /// 9 * 9 스도쿠 타입
        /// </summary>
        NINE_NINE,
    }

    public GameObject prefabNumberItem;
    public Transform createParent_portrait; // 가로용, 세로용이 필요하다.

    public void CreateMatrix(MatrixType _type)
    {
        for (int i = 0; i <= 80; i++)
        {
            GameObject _instance = GameObject.Instantiate(prefabNumberItem, createParent_portrait) as GameObject;
            _instance.name = (i + 1).ToString();
        }
    }
}
