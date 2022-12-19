using System;
using UnityEngine;
public class TriggerHandler : MonoBehaviour
{
    public static TriggerHandler singleton { get; private set; }

    public Action wallAction;
    public Action coinAction;
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
    private void OnTriggerEnter(Collider other)
    {
        var coin = other.GetComponent<Coin>();

        if (coin != null)
        {
            coin.GetCoin();
            coinAction?.Invoke();
        }
        else
        {
            wallAction();
        }
    }
}
