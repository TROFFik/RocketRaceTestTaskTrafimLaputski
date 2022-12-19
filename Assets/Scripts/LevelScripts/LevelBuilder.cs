using UnityEngine;
using System.Collections.Generic;
public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private float tunnelWidth;
    [SerializeField] private float minGapWidth;
    [SerializeField] private float shiftSize;
    [Space(20)]
    [SerializeField] private LevelBrick[] levelBrickPrefabs = new LevelBrick[2];

    private LevelMovementGeneration levelMove;
    private List<LevelBrick> levelBricks = new List<LevelBrick>();

    private Vector2 deletePoint;
    private Vector2 buildPoint;
    private void Start()
    {
        BuildLevel();
        SetDataToLevelMove();
    }

    private void BuildLevel()
    {
        buildPoint.y = (int)Camera.main.ScreenToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z)).y + shiftSize;
        deletePoint.y = -buildPoint.y;

        while (buildPoint.y >= deletePoint.y)
        {
            for (int i = 0; i < levelBrickPrefabs.Length; i++)
            {
                var TempBrick = Instantiate(levelBrickPrefabs[i], buildPoint, Quaternion.identity, transform);

                TempBrick.GetLevelDataParameters(tunnelWidth, minGapWidth);
                TempBrick.GenerateBrick(false);
                levelBricks.Add(TempBrick);

                buildPoint.y -= shiftSize;
            }
        }
        deletePoint.y = buildPoint.y + 3;
        buildPoint.y *= -1;
    }

    private void SetDataToLevelMove()
    {
        levelMove = LevelMovementGeneration.singleton;
        levelMove.GetLevelDataParameters(deletePoint, buildPoint, levelBricks);
    }
}
