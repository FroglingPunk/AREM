using System.Collections;
using UnityEngine;

public class CoroutinesRunner : MonoBehaviour
{
    private static CoroutinesRunner _instance;
    public static CoroutinesRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CoroutinesRunner").AddComponent<CoroutinesRunner>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }


    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}