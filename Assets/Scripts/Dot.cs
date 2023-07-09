using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    private int dotNumber;
    private Color textColor;
    private TextMesh text;
    private SpriteRenderer sr;
    private ReadData.LevelList levels;

    public Sprite clickedDot;
    public GameObject textObject;
    public static List<Transform> dotPositions;

    public static event Action onConnectDots;

    public float orbitRadius = 0.5f;
    public float orbitSpeed = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        dotPositions = new List<Transform>();
        levels = ReadData.levels;
        sr = GetComponent<SpriteRenderer>();
        dotNumber = Int32.Parse(Regex.Match(this.name, @"\d+").Value);
        text = textObject.GetComponent<TextMesh>();
        text.text = dotNumber.ToString();
    }


    IEnumerator Fade()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.1f)
        {
            textColor = text.color;
            textColor.a = f;
            text.color = textColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void OnMouseDown()
    {
        if (dotNumber - CorrectDotCount.correctDotCount == 1)
        {
            sr.sprite = clickedDot;
            CorrectDotCount.correctDotCount = dotNumber;
            StartCoroutine("Fade");
            dotPositions.Add(transform);

            if (dotNumber == GenerateDots.dotCount)
            {
                dotPositions.Add(dotPositions[0]);
            }

            if (dotPositions.Count >= 2)
            {
                onConnectDots?.Invoke();
            }
        }
    }
}
