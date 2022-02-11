using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Enums;

public class ScoreObject : MonoBehaviour
{
    public Text scoreText;

    private int _score;

    public int Score
    {
        get => _score;
        private set
        {
            _score += value;
            scoreText.text = _score.ToString();
        }
    }



    public void AddPoints( CircleStage stage )
    {
        int stageDifference = CircleStage.E - stage;
        Score = 5 + stageDifference * stageDifference * 25;
    }
}
