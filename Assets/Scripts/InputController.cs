using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController singleton { get; private set; }

    public Action<Vector2> playerMoveAction;
    public Action<bool> �lickAction;

    private Vector2 startPos;
    private Vector2 direction;

    bool isGame = true;
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

    private void Start()
    {
        PlayerHealth.singleton.DeadAction += IsGame;
    }
    private void Update()
    {
        if (isGame)
        {
            if (Input.touchCount > 0)
            {
                Input�alculations();

                �lickAction?.Invoke(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                �lickAction?.Invoke(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                �lickAction?.Invoke(false);
            }
        }
        else
        {
            �lickAction?.Invoke(false);
        }
    }

    private void Input�alculations()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;

            case TouchPhase.Moved:
                direction.x = 2 * (touch.position.x - startPos.x) / Screen.width;
                direction.x = Mathf.Clamp(direction.x, -1, 1);

                playerMoveAction?.Invoke(direction);
                break;
        }
    }

    private void IsGame()
    {
        isGame = false;
    }
}