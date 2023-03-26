using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyQte : MonoBehaviour
{
    private static GameObject _instance;
    public static GameObject Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = gameObject;
        DontDestroyOnLoad(gameObject);
    }

}
