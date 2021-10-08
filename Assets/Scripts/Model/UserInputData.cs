using System;

[Serializable]
public class UserInputData
{
    public int length;
    /// <summary>
    /// 사용자가 입력하지 않은 값은 0
    /// </summary>
    public int[,] inputData;

    public UserInputData(int _length, int[,] _data)
    {
        length = _length;
        inputData = _data;
    }

    /// <summary>
    /// 내부에서 이중 포문 사용하므로 호출시 주의.
    /// </summary>
    public void InitValue()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                inputData[i, j] = 0;
            }
        }
    }

    
}
