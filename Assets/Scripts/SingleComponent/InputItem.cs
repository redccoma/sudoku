using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputItem : MonoBehaviour
{
    public InputField field;

    public int GetData()
    {
        return int.Parse(field.text);
    }

    public void SetData(int number)
    {
        field.text = number.ToString();
    }

    public void SetData(string str)
    {
        field.text = str;
    }
}
