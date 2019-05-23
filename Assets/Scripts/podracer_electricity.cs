using UnityEngine;
using System.Collections;

public class podracer_electricity : MonoBehaviour
{
    // Code from https://www.youtube.com/watch?v=Adc616Q1VoY
    private LineRenderer lineRend;
    private Vector3[] points = new Vector3[5];

    private readonly int point_begin = 0;
    private readonly int point_mid_left = 1;
    private readonly int point_center = 2;
    private readonly int point_mid_right = 3;
    private readonly int point_end = 4;

    public Transform line;
    private readonly float randomPosOffset = 0.3f;
    private readonly float randomWithOffsetMax = 2f;
    private readonly float randomWithOffsetMin = 1f;

    private readonly WaitForSeconds customFrame = new WaitForSeconds(0.5f);

    private void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        StartCoroutine(Beam());
    }

    private IEnumerator Beam()
    {
        yield return customFrame;
        points[point_begin] = transform.position;
        points[point_end] = line.position;
        CalculateMiddle();
        lineRend.SetPositions(points);
        lineRend.SetWidth(RandomWidthOffset(), RandomWidthOffset());
        StartCoroutine(Beam());
    }

    private float RandomWidthOffset()
    {
        return Random.Range(randomWithOffsetMin, randomWithOffsetMax);
    }

    private void CalculateMiddle()
    {
        Vector3 center = GetMiddleWithRandomness(transform.position, line.position);
        points[point_center] = center;
        points[point_mid_left] = GetMiddleWithRandomness(transform.position, center);
        points[point_mid_right] = GetMiddleWithRandomness(center, line.position);
    }

    private Vector3 GetMiddleWithRandomness(Vector3 point1, Vector3 point2)
    {
        float x = (point1.x + point2.x) / point_center;
        float finalX = Random.Range(x - randomPosOffset, x + randomPosOffset);
        float y = (point1.y + point2.y) / point_center;
        float finalY = Random.Range(y - randomPosOffset, y + randomPosOffset);
        return new Vector3(finalX, finalY, 0);
    }
}
