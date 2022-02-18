using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;
using Enums;

public class PointsObject : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    [SerializeField] private Animation animationControl;



    public void Initiate( int points, CircleStage stage )
    {
        pointsText.text = ( points > 0 ? "+" : "" ) + points;
        pointsText.color = CIRCLE.STAGE_COLOR[ (int)stage ];
        animationControl.Play();
        Destroy( gameObject, 1.0f );
    }
}
