using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;
using Enums;

public class PointsObject : MonoBehaviour
{
    public Text pointsText;
    public Animation animationControl;



    public void Initiate( int points, CircleStage stage )
    {
        pointsText.text = ( points > 0 ? "+" : "" ) + points;
        pointsText.color = CIRCLE.STAGE_COLOR[ (int)stage ];
        animationControl.Play();
        Destroy( gameObject, 1.0f );
    }
}
