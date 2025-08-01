using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class SinglatonForMono<T> : MonoBehaviour where T : SinglatonForMono<T>
{
    public static T instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Initial();
    }

    protected virtual void Initial()
    {

    }
}