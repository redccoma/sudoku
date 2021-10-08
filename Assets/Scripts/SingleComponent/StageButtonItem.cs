using UnityEngine;
using UnityEngine.UI;

public class StageButtonItem : MonoBehaviour
{
    public int questionNumber;
    public Text mText;

    public GameObject GetGameObject
    {
        get
        {
            return transform.gameObject;
        }
    }

    public void SetData(int number)
    {
        questionNumber = number;
        mText.text = number.ToString();
    }

    public void OnClick()
    {
        DataManager.Instance.GoStage(questionNumber);
    }
}
