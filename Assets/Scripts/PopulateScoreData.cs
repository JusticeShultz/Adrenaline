using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScoreData : MonoBehaviour
{
    public Text scoreDisplay;
    public Text timeDisplay;

    void Start()
    {
        GameObject scoreObject = GameObject.Find("Score");
        GameObject timeObject = GameObject.Find("TimeText");

        if (!scoreObject)
        {
            Destroy(scoreDisplay);
            Destroy(timeDisplay);
            return;
        }
        if (!timeObject)
        {
            Destroy(scoreDisplay);
            Destroy(timeDisplay);
            return;
        }

        scoreDisplay.text = scoreObject.GetComponent<Text>().text;
        timeDisplay.text = timeObject.GetComponent<Text>().text;

        Destroy(scoreObject);
        Destroy(timeObject);
    }
}