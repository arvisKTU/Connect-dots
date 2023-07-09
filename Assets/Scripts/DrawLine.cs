using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private const float ANIMATION_DURATION = 5f;
    private const float FINISH_DELAY = 1f;

    public LineRenderer lineRendererPrefab;
    private List<Transform> dotPositions;
    private int currentIndex;

    public static event Action onFinishLevel;

    private void Awake()
    {
        Dot.onConnectDots += DrawLineBetweenDots;
    }

    private void OnDestroy()
    {
        Dot.onConnectDots -= DrawLineBetweenDots;
    }

    private void DrawLineBetweenDots()
    {
        dotPositions = Dot.dotPositions;
        currentIndex = 0;

        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {
        int pointsCount = dotPositions.Count;
        Vector3 lastLineEndPosition = dotPositions[pointsCount - 1].position;
        Vector3 firstDotPosition = dotPositions[0].position;
        float segmentDuration = ANIMATION_DURATION / (GenerateDots.dotCount - 1);

        LineRenderer lineRenderer = null;

        for (int i = currentIndex; i < pointsCount - 1; i++)
        {
            Vector3 startPosition = dotPositions[i].position;
            Vector3 endPosition = dotPositions[i + 1].position;

            if (lineRenderer == null)
            {
                lineRenderer = CreateLineRenderer();
            }
            if (i == GenerateDots.dotCount - 1)
            {
                lineRenderer = CreateLineRenderer();
            }

            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);

            float timer = 0f;

            while (timer < segmentDuration)
            {
                timer += Time.deltaTime;
                float t = timer / segmentDuration;
                Vector3 pos = Vector3.Lerp(startPosition, endPosition, t);
                lineRenderer.SetPosition(1, pos);
                yield return null;
            }
            currentIndex++;
        }
        if(lastLineEndPosition == firstDotPosition)
        {
            yield return new WaitForSeconds(FINISH_DELAY);
            onFinishLevel?.Invoke();
        }
    }

    private LineRenderer CreateLineRenderer()
    {
        LineRenderer newLineRenderer = Instantiate(lineRendererPrefab, transform);
        return newLineRenderer;
    }
}
