using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScoreData : MonoBehaviour
{
    public int score = 0;
    public Text scoreDisplay;
    public Text timeDisplay;
    public GameObject bronzeBadge;
    public GameObject silverBadge;
    public GameObject goldBadge;

    void Start()
    {
        GameObject scoreObject = GameObject.Find("Score");
        GameObject timeObject = GameObject.Find("TimeText");

        if (!scoreObject)
        {
            Destroy(scoreDisplay.gameObject);
            Destroy(timeDisplay.gameObject);
            return;
        }
        if (!timeObject)
        {
            Destroy(scoreDisplay.gameObject);
            Destroy(timeDisplay.gameObject);
            return;
        }

        scoreDisplay.text = scoreObject.GetComponent<Text>().text;
        timeDisplay.text = timeObject.GetComponent<Text>().text;

        score = scoreObject.GetComponent<ScoreOutput>().score;

        if (score >= 1000000)
        {
            if (score >= 5000000)
            {
                goldBadge.SetActive(true);
            }
            else silverBadge.SetActive(true);
        }
        else bronzeBadge.SetActive(true);

        Destroy(scoreObject);
        Destroy(timeObject);
    }
}