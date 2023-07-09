using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDots : MonoBehaviour
{
    private ReadData.LevelList levels;

    public GameObject dot;
    public static int dotCount;

    void Start()
    {
        levels = ReadData.levels;
        SpawnDots(Menu.currentLevel);
        dotCount = levels.levels[Menu.currentLevel].xCoordinates.Count;
    }

    private void SpawnDots(int level)
    {
        for (int i=0;i<levels.levels[level].xCoordinates.Count;i++)
        {  
            GameObject dotClone = Instantiate(dot, new Vector3(levels.levels[level].xCoordinates[i], levels.levels[level].yCoordinates[i], 0), Quaternion.identity) as GameObject;
            dotClone.name += i + 1;
        }
    }
}
