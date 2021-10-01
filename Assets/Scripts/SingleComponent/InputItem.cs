using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputItem : MonoBehaviour
{
    public InputField field;
    public Toggle toggle;

    public string Name
    {
        get
        {
            return transform.name;
        }
    }

    public bool IsQuestion
    {
        get
        {
            return toggle.isOn;
        }
    }

    public void SetInit()
    {
        field.text = string.Empty;
        toggle.isOn = false;
    }

    public int GetData()
    {
        return string.IsNullOrEmpty(field.text) ? 0 : int.Parse(field.text);
    }

    public void SetData(int number, bool isQuestion)
    {
        field.text = number.ToString();
        toggle.isOn = isQuestion;
    }

    public void SetData(string str)
    {
        field.text = str;
    }
}
