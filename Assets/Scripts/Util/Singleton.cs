using UnityEngine;

/// <summary>
/// Awake 구현이 필요할 경우 override로 구현할것
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj;

                obj = GameObject.Find(typeof(T).Name);

                if (obj == null)
                {
                    obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                else
                {
                    instance = obj.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}