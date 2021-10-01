using System;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

[Serializable]
public class SudokuModel
{
    [Serializable]
    public class Question
    {
        private const short INVALID_VALUE = -1;

        public enum JsonType
        {
            /// <summary>
            /// 문제
            /// </summary>
            Question,
            /// <summary>
            /// 정답
            /// </summary>
            Answer,
        }

        /// <summary>
        /// 가로 혹은 세로에 대한 길이
        /// </summary>
        public int length;

        /// <summary>
        /// 문제는 0인덱스, 문제+사용자데이터는 1인덱스
        /// </summary>
        public int[,] q;
        /// <summary>
        /// 
        /// </summary>
        public int[,] a;

        /// <summary>
        /// 현재 올바른 데이터로 전부 입력되었는지?
        /// </summary>
        public bool IsCorrectTotalData()
        {
            bool result = true;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (q[i, j] == Question.INVALID_VALUE || a[i, j] == Question.INVALID_VALUE)
                    {
                        result = false;
                        break;
                    }
                }

                if (!result)
                    break;
            }

            return result;
        }

        /// <summary>
        /// 생성자. 데이터 생성시 길이 필수
        /// </summary>
        /// <param name="length"></param>
        public Question(int _length)
        {
            length = _length;

            q = new int[length, length];
            a = new int[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    q[i, j] = Question.INVALID_VALUE;
                    a[i, j] = Question.INVALID_VALUE;
                }
            }
        }

        /// <summary>
        /// 특정 위치마다 데이터 입력
        /// </summary>
        /// <param name="_type">입력하려는 데이터의 성격</param>
        /// <param name="x">index X 가로 (1 ~ length중 1택)</param>
        /// <param name="y">index Y 세로 (1 ~ length중 1택)</param>
        /// <param name="data">스도쿠 정답데이터 (사용자 문제로 출제할 경우 0)</param>
        /// <returns>정상적용 여부</returns>
        public bool SetData(JsonType _type, short x, short y, int data)
        {
            // x나 y의 값이 1 ~ length - 1 사이의 값이어야 한다.
            if (x < 0 || length - 1 < x)
                return false;

            if (y < 0 || length - 1 < y)
                return false;

            if (_type == JsonType.Question)
                q[x, y] = data;            
            else if (_type == JsonType.Answer)
                a[x, y] = data;

            return true;
        }

        /// <summary>
        /// 사용자 입력값의 정답 여부
        /// </summary>
        /// <param name="x">index X 가로 (1 ~ length중 1택)</param>
        /// <param name="y">index Y 세로 (1 ~ length중 1택)</param>
        /// <param name="userData">사용자 입력 데이터</param>
        /// <returns>정답여부</returns>
        public bool CheckAnswer(short x, short y, int userData)
        {
            return a[x, y] == userData;
        }

        /// <summary>
        /// 특정 위치 값이 문제용 숫자인가?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsQuestionItem(short x, short y)
        {
            return q[x, y] > Question.INVALID_VALUE;    // q배열 데이터값이 초기값보다 크다면 문제임.
        }
    }

    /// <summary>
    /// 키: 문제번호, 값:문제데이터
    /// </summary>
    public Dictionary<int, Question> data = new Dictionary<int, Question>();

    /// <summary>
    /// 생성된 문제번호가 있는지
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsContainKey(int key)
    {
        return data.ContainsKey(key);
    }

    public Question GetQuestion(int key)
    {
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        else
            return null;
    }

    public void AddData(int key, Question _data)
    {
        if(data.ContainsKey(key))
        {
            Debug.LogError("키 있음.");
        }
        else
        {
            data.Add(key, _data);
            Debug.Log(string.Format("<color=white>{0}</color>", "데이터 추가 완료"));
        }
    }
}