﻿using System;

[Serializable]
public class Question
{
    private const short INVALID_VALUE = -1;

    /// <summary>
    /// 가로 혹은 세로에 대한 길이
    /// </summary>
    public int length;

    /// <summary>
    /// 문제 생성시 체크박스로 처리된 붉은색 숫자들 (그외 좌표는 -1)
    /// 이 데이터는 인게임에서 최초 문제 셋팅에서 사용된다.
    /// </summary>
    public int[,] q;

    /// <summary>
    /// 모든 숫자가 입력된 데이터
    /// </summary>
    public int[,] a;

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
    /// <param name="x">index X 가로 (0 ~ length-1중 1택)</param>
    /// <param name="y">index Y 세로 (0 ~ length-1중 1택)</param>
    /// <param name="data">스도쿠 정답데이터 (사용자 문제로 출제할 경우 0)</param>
    /// <returns>정상적용 여부</returns>
    public bool SetData(JSON_TYPE _type, short x, short y, int data)
    {
        // x나 y의 값이 1 ~ length - 1 사이의 값이어야 한다.
        if (x < 0 || length - 1 < x)
            return false;

        if (y < 0 || length - 1 < y)
            return false;

        if (_type == JSON_TYPE.Question)
            q[x, y] = data;
        else if (_type == JSON_TYPE.Answer)
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
    /// 특정 위치 값이 문제(최초 출력용도)용 숫자인가?
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsQuestionItem(short x, short y)
    {
        return q[x, y] > Question.INVALID_VALUE;    // q배열 데이터값이 초기값보다 크다면 문제임.
    }

    /// <summary>
    /// 문제 데이터 수정과정에서 문제였다가 정답으로 바뀌면 q데이터 수정이 필요하여 만든다.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void ResetQuestionData(short x, short y)
    {
        q[x, y] = Question.INVALID_VALUE;
    }
}