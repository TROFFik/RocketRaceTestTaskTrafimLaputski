using UnityEngine;

public class LevelBrick : MonoBehaviour
{
    [SerializeField] private GameObject[] smallLevelBricks = new GameObject[3];

    private float tunnelWidth;
    private float minGapWidth;
    float CoordinateSignMultiplier;
    public void GetLevelDataParameters(float TunnelWidth, float MinGapWidth)
    {
        CoordinateSignMultiplier = Mathf.Clamp(smallLevelBricks[0].transform.position.x, -1, 1);
        tunnelWidth = TunnelWidth;
        minGapWidth = MinGapWidth;
    }
    public Vector2 GenerateCoin()
    {
        return Vector2.Lerp(smallLevelBricks[0].transform.position, smallLevelBricks[1].transform.position, 0.5f);
    }

    public void GenerateBrick(bool UseGap)
    {
        if (UseGap)
        {
            float XBrickCoordinate = Random.Range(tunnelWidth / 2 * CoordinateSignMultiplier, 2 * CoordinateSignMultiplier);
            smallLevelBricks[0].transform.position = new Vector3(XBrickCoordinate, smallLevelBricks[0].transform.position.y);

            for (int i = 1; i < smallLevelBricks.Length; i++)
            {
                float Gap = Random.Range(minGapWidth, Vector2.Distance(smallLevelBricks[0].transform.position, new Vector2((tunnelWidth / 2 * -CoordinateSignMultiplier), smallLevelBricks[0].transform.position.y)));
                smallLevelBricks[i].transform.position = new Vector3(XBrickCoordinate + Gap * -CoordinateSignMultiplier, smallLevelBricks[i].transform.position.y);
            }
        }
        else
        {
            float XBrickCoordinate = tunnelWidth / 2 * CoordinateSignMultiplier;
            smallLevelBricks[0].transform.position = new Vector3(XBrickCoordinate, smallLevelBricks[0].transform.position.y);

            for (int i = 1; i < smallLevelBricks.Length; i++)
            {
                smallLevelBricks[i].transform.position = new Vector3(-XBrickCoordinate, smallLevelBricks[i].transform.position.y);
            }
        }
    }
}
