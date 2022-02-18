using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Constants;
using Enums;

public class CircleObject : MonoBehaviour
{
    public RectTransform rectTransform;

    [SerializeField] private Button button;
    [SerializeField] private Image outlineImage;
    [SerializeField] private Image timerImage;
    [SerializeField] private Image centreImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private CircleStage _stage = CircleStage.X;
    private CircleStage _startStage = CircleStage.A;
    private CircleDifficulty _difficulty = CircleDifficulty.Easy;
    private float _duration = 1.5f;
    private float _speed = 1.0f;
    private float _time = 0.0f;
    private Action<CircleObject> _destroyAction;

    public CircleStage Stage { get => _stage; }
    public CircleDifficulty Difficulty { get => _difficulty; }



    public void Initiate( Action<CircleObject> destroyAction, CircleDifficulty difficulty = CircleDifficulty.Easy, float speedRatio = 1.0f )
    {
        // _startStage    = startStage;
        _speed         = ( 1.0f + ( (float)difficulty - 1.0f ) * 0.5f ) * speedRatio;
        _destroyAction = destroyAction;
        _difficulty    = difficulty;

        rectTransform.sizeDelta = rectTransform.sizeDelta * (float)difficulty;
    }


    private void OnEnable() {
        button.onClick.AddListener( OnButtonClicked );
    }

    private void OnDisable() {
        button.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        float percentageOfMaxValue = 1.0f - _time / ( _duration / _speed );

        if ( percentageOfMaxValue > 0.0f )
        {
            CircleStage stage = (CircleStage)Math.Ceiling( (float)_startStage * percentageOfMaxValue );
            timerImage.fillAmount = percentageOfMaxValue;
            SetStage( stage );
        }
        else
        {
            SetStage( CircleStage.X );
        }
    }

    private void SetStage( CircleStage stage )
    {
        _stage             = stage;
        timerText.text     = stage.ToString();
        outlineImage.color = CIRCLE.STAGE_COLOR[ (int)stage ];
        centreImage.color  = CIRCLE.STAGE_COLOR[ (int)stage ];
    }

    private void OnButtonClicked()
    {
        _destroyAction( this );
        Destroy( gameObject );
    }
}
