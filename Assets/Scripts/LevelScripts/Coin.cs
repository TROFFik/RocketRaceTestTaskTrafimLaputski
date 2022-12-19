using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Coin : MonoBehaviour
{    
    PoolObject poolObject;

    bool InvisibleQuit = false;
    private void Start()
    {
        poolObject = GetComponent<PoolObject>();
    }

    private void OnBecameInvisible()
    {
        if (!InvisibleQuit)
        {
            poolObject.RetunToPool();
        }
    }

    public void GetCoin()
    {
        poolObject.RetunToPool();
    }

    private void OnApplicationQuit()
    {
        InvisibleQuit = true;
    }
}
