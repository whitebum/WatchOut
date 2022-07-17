using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class
{
    private static T _instance = null;
    private static object _lock = new object();
    private static bool isInstanceDestroy = false;

    public static T GetInstance()
    {
        if (isInstanceDestroy)
        {
            return null;
        }

        if (_instance == null)
        {
            lock (_lock)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).ToString()).AddComponent(typeof(T)) as T;
                    }
                }
            }
        }

        return _instance;
    }

    // 인스턴스 삭제 후에도 호출되는 것을 막기 위함.
    protected virtual void OnApplicationQuit()
    {
        isInstanceDestroy = true;
    }

    protected virtual void OnDestroy()
    {
        isInstanceDestroy = true;
    }
}