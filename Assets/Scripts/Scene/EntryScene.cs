using UnityEngine;

public class EntryScene : MonoBehaviour
{
    public GameObject dontDestroyObject;

    public void Awake()
    {
        DontDestroyOnLoad(dontDestroyObject);

    }

    public void Start()
    {
        DataManager.Instance.Init(OnDataLoaded);
    }

    public void OnDataLoaded(bool isSuccess)
    {
        if (isSuccess)
        {
            SceneManager.Instance.ChangeScene(SCENE_TYPE.Lobby);
        }
        else
        {
            // TODO
            // 데이터 초기화 실패시 처리 
        }
    }

}