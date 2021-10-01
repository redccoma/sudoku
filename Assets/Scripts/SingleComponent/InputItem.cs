using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputItem : MonoBehaviour
{
    public InputField field;
    public Toggle toggle;
    public Text numberText;

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

        if (toggle.isOn)
            numberText.color = Color.red;
        else
            numberText.color = Color.black;
    }

    public void SetData(string str)
    {
        field.text = str;
    }

    public void OnValueChangedToggle()
    {
        if(toggle.isOn)
            numberText.color = Color.red;
        else
            numberText.color = Color.black;
    }
}
