using System;
using System.Collections.Generic;

/// <summary>
/// 사용자 입력 데이터를 항상 유지하기 위함.
/// </summary>
[Serializable]
public class UserModel
{
    /// <summary>
    /// 키: 문제번호, 값: 사용자 입력값 (문제 및 정답은 0)
    /// </summary>
    public Dictionary<int, UserInputData> userData = new Dictionary<int, UserInputData>();

    /// <summary>
    /// 생성시엔 스도쿠 기본 모델 데이터를 받아 셋팅한다.
    /// </summary>
    /// <param name="sm"></param>
    public UserModel(SudokuModel model)
    {
        if(model != null)
        {
            Dictionary<int, Question>.KeyCollection keys = model.data.Keys;

            foreach (int key in keys)
            {
                int length = model.data[key].length;
                int[,] value = new int[length, length];

                // 빠른 처리를 위해 초기화를 -1등으로 재처리하지 않고 0으로 그대로 둔다.
                userData.Add(key, new UserInputData(length, value));
            }
        }
        else
        {
            // TODO
            // 예외처리는 추후
        }
    }

    /// <summary>
    /// 문제 데이터를 기반으로 현재 유저 데이터를 검증한다.
    /// 각 문제번호별로 유저데이터가 정확히 입력되었는지만 검증하고
    /// 검증 실패시 문제번호에 맞는 배열만 생성후 값은 초기화한다.
    /// </summary>
    public void VerifyData(SudokuModel model)
    {
        if(model != null)
        {
            Dictionary<int, Question>.KeyCollection originKeys = model.data.Keys;

            // 이쪽에 전체데이터를 검증하는 로직을 만들기에는 3중 포문이 사용되므로,
            // 퍼포먼스 측면에서 아래의 문제데이터에 대한 검증만 처리하고,
            // 유저데이터 숫자 검증은 인게임 진입시 문제번호별로 1건씩 처리한다.

            // 원형데이터에 없는 문제번호를 유저데이터가 들고있다면 삭제한다.
            // 유저데이터키 - 원형데이터키 = 삭제할 키
            List<int> targetList = new List<int>();
            foreach (int key in userData.Keys)
                targetList.Add(key);

            for (int i = targetList.Count-1; i >= 0; i--)
            {
                int targetKey = targetList[i];
                // 원형데이터에 있는 키면 targetList에서 삭제.
                if (model.data.ContainsKey(targetKey))
                    targetList.RemoveAt(i);
            }

            if(targetList.Count > 0)
            {
                // targetList에 있는 키데이터는 삭제.
                for (int i = 0; i < targetList.Count; i++)
                    userData.Remove(targetList[i]);
            }
        }
    }

    /// <summary>
    /// 특정 문제의 유저데이터 혹은 전체 유저데이터를 초기화한다.
    /// 전체 초기화일 경우 데이터가 많을 때 프리징 현상이 있을수도 있으므로 주의.
    /// </summary>
    /// <param name="questionNumber">0, 즉 매개변수 없이 호출시 모든 유저데이터를 초기화한다.</param>
    public void InitData(int questionNumber = 0)
    {
        if(userData.ContainsKey(questionNumber))
        {
            userData[questionNumber].InitValue();
        }
        else
        {
            if (questionNumber == 0)
            {
                foreach (var item in userData)
                {
                    // 전체 초기화..데이터가 많으면 오래걸릴수 있음.
                    item.Value.InitValue();
                }
            }
            else
            { 
                // 그외 키가 없으면 별도의 액션은 없어도 될듯.
            }
        }
    }
}