using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int currentLevel;

    private void Start()
    {
        DrawLine.onFinishLevel += LoadLevelAfterComplete;
        currentLevel = 0;
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(1);
    }

    public void LoadLevelAfterComplete()
    {
        SceneManager.LoadScene(0);
    }
 
}
