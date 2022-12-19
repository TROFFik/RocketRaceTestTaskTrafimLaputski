using System.Collections.Generic;
using UnityEngine;

public class LevelMovementGeneration: MonoBehaviour
{
    public static LevelMovementGeneration singleton { get; private set; }

    [SerializeField] private int maxMoneyLineCount;
    [SerializeField] private float speed;
    [SerializeField] private Pool pool;
    private bool isMotion;

    private List<LevelBrick> levelBricks = new List<LevelBrick>();

    private Vector2 deletePoint;
    private Vector2 buildPoint;

    private int currentMoneyLineCount;

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
        InputController.singleton.ñlickAction += IsMotion;
    }

    private void Update()
    {
        if (isMotion)
        {
            MoveBrick();
        }
    }

    private void IsMotion(bool Value)
    {
        isMotion = Value;
    }
    private void MoveBrick()
    {
        foreach (var brick in levelBricks)
        {
            brick.transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (brick.transform.position.y <= deletePoint.y)
            {
                brick.transform.position = buildPoint;

                int Temp = Random.Range(0, 3);

                if (Temp == 2)
                {
                    brick.GenerateBrick(true);
                }
                else
                {
                    brick.GenerateBrick(false);
                }

                if (currentMoneyLineCount == 0)
                {
                    Temp = Random.Range(0, 10);

                    if (Temp == 9)
                    {
                        currentMoneyLineCount = Random.Range(1, maxMoneyLineCount);
                    }
                }
                else
                {
                    currentMoneyLineCount--;

                    pool.GetFreeObject(brick.GenerateCoin(), brick.transform);
                }
            }
        }
    }

    public void GetLevelDataParameters(Vector2 DeletePoint, Vector2 BuildPoint, List<LevelBrick> LevelBricks)
    {
        deletePoint = DeletePoint;
        buildPoint = BuildPoint;
        levelBricks = LevelBricks;
    }
}
