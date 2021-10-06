using System;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

[Serializable]
public class SudokuModel
{
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