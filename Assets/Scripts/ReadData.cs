using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    public TextAsset jsonText;
    public GameObject dot;

    [System.Serializable]
    public class LevelData
    {
        public int[] level_data;
        public List<float> xCoordinates;
        public List<float> yCoordinates;
    }

    [System.Serializable]
    public class LevelList
    {
        public LevelData[] levels;
    }

    public static LevelList levels;

    void Awake()
    {
        levels = new LevelList();
        levels = JsonUtility.FromJson<LevelList>(jsonText.text);
        FormatData();
    }

    void FormatData()
    {
        float dotSize = dot.transform.localScale.x;

        float minXCoordinate = float.MaxValue;
        float minYCoordinate = float.MaxValue;
        float maxXCoordinate = float.MinValue;
        float maxYCoordinate = float.MinValue;

        for (int i = 0; i < levels.levels.Length; i++)
        {
            for (int j = 0; j < levels.levels[i].level_data.Length; j++)
            {
                if (j == 0 || j % 2 == 0)
                {
                    float coordinateX = levels.levels[i].level_data[j];
                    minXCoordinate = Mathf.Min(minXCoordinate, coordinateX) - dotSize;
                    maxXCoordinate = Mathf.Max(maxXCoordinate, coordinateX) + dotSize;
                }
                else
                {
                    float coordinateY = levels.levels[i].level_data[j];
                    minYCoordinate = Mathf.Min(minYCoordinate, coordinateY) - dotSize;
                    maxYCoordinate = Mathf.Max(maxYCoordinate, coordinateY) + dotSize;
                }
            }
        }

        float dotPictureSize = Mathf.Max(maxXCoordinate - minXCoordinate, maxYCoordinate - minYCoordinate);

        Vector3 cameraPosition = Camera.main.transform.position;
        float cameraSize = Camera.main.orthographicSize;
        Vector3 centerPosition = new Vector3(cameraPosition.x, cameraPosition.y, 0f);

        float scaleFactor = cameraSize * 2f / dotPictureSize;

        float xOffset = (maxXCoordinate - minXCoordinate) * scaleFactor * 0.5f;
        float yOffset = (maxYCoordinate - minYCoordinate) * scaleFactor * 0.5f;

        centerPosition.x -= xOffset;
        centerPosition.y += yOffset;

        for (int i = 0; i < levels.levels.Length; i++)
        {
            for (int j = 0; j < levels.levels[i].level_data.Length; j += 2)
            {
                float coordinateX = (levels.levels[i].level_data[j] - centerPosition.x) * scaleFactor;
                float coordinateY = (centerPosition.y - levels.levels[i].level_data[j + 1]) * scaleFactor;

                levels.levels[i].xCoordinates.Add(coordinateX);
                levels.levels[i].yCoordinates.Add(coordinateY);
            }
        }
    }

}
