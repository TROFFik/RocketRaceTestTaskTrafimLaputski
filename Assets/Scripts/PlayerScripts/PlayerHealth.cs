using UnityEngine;
using System;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton { get; private set; }

    public Action DeadAction;

    bool InvisibleQuit = false;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        TriggerHandler.singleton.wallAction += IsDead;
    }

    private void OnBecameInvisible()
    {
        if (!InvisibleQuit)
        {
            IsDead();
        }
    }

    void IsDead()
    {
        DeadAction?.Invoke();
    }

    private void OnApplicationQuit()
    {
        InvisibleQuit = true;
    }
}
