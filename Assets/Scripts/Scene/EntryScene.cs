using UnityEngine;

public class EntryScene : MonoBehaviour
{
    public GameObject dontDestroyObject;

    public void Awake()
    {
        DontDestroyOnLoad(dontDestroyObject);
    }
}