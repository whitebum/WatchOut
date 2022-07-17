using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantSingleton<T> : MonoBehaviour where T : class
{
    private static T instance = null;

    public static T GetInstance()
    {
        if (instance != null)
            return instance;

        return null;
    }

    private void Awake()
    {
        if (instance == null)
            instance = FindObjectOfType(typeof(T)) as T;
    }

    public void OnDestroy()
    {
        instance = null;
    }
}
