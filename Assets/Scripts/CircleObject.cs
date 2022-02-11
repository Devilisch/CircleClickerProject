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
    public Button button;
    public Image outlineImage;
    public Image timerImage;
    public Image centreImage;
    public TextMeshProUGUI timerText;

    private CircleStage _stage = CircleStage.X;
    private CircleStage _startStage = CircleStage.E;
    private List<Color> _stageColor = new List<Color>{ COLOR.BLACK, COLOR.RED, COLOR.ORANGE, COLOR.YELLOW, COLOR.LIGHT_GREEN, COLOR.DARK_GREEN };
    private float _duration = 3.0f;
    private float _speed = 1.0f;
    private float _time = 0.0f;
    private Action<CircleStage> _destroyAction;



    public void Initiate( Action<CircleStage> destroyAction, CircleStage startStage = CircleStage.E, float speed = 1.0f, float duration = 3.0f )
    {
        _startStage    = startStage;
        _speed         = speed;
        _duration      = duration;
        _destroyAction = destroyAction;
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
        outlineImage.color = _stageColor[ (int)stage ];
        centreImage.color  = _stageColor[ (int)stage ];
    }

    private void OnButtonClicked()
    {
        _destroyAction( _stage );
        Destroy( gameObject );
    }
}
