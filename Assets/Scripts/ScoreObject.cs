using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Enums;

public class ScoreObject : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreShadowText;
    [SerializeField] private GameObject pointsObject;
    [SerializeField] private RectTransform pointsSpawnTransform;

    private int _score;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            scoreText.text = _score.ToString();
            scoreShadowText.text = _score.ToString();
        }
    }



    public void AddPoints( CircleObject circle )
    {
        int difficaltyCoefficient = circle.Difficulty - CircleDifficulty.Easy;
        int points = ( circle.Stage - CircleStage.E ) * ( 5 * difficaltyCoefficient * difficaltyCoefficient + 10 * difficaltyCoefficient + 10  );
        Score += points;
        PointsObject pointsObjectNew = Instantiate( pointsObject, pointsSpawnTransform, false ).GetComponent<PointsObject>();
        pointsObjectNew.transform.localPosition = circle.transform.localPosition;
        pointsObjectNew.Initiate( points, circle.Stage );
    }
}
