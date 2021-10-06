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
        // 만약 Entry에서 처리해야할 부분이 있다면 처리.
        SceneManager.Instance.ChangeScene(SCENE_TYPE.Lobby);
    }
}