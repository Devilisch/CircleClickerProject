using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Enums;

public class SpawnSystem : MonoBehaviour
{
    public RectTransform spawnTransform;

    [SerializeField] private GameObject circleObject;
    [SerializeField] private ScoreObject scoreObject;
    [SerializeField] private ChangeButtons populationButtons;
    [SerializeField] private Button pauseButton;
    [SerializeField] private ChangeButtons speedButtons;
    [SerializeField] private GameObject pauseObject;

    private int _maxCirclesOnScreen = 2;
    private float _spawnCooldown = 0.2f;
    private float _time = 0.0f;
    private float _speedRatio = 1.0f;
    private bool _isPaused = false;



    private void OnEnable()
    {
        pauseButton.onClick.AddListener( OnPauseButtonClicked );

        populationButtons.DefaultText = "Circles x";
        populationButtons.Value = 2;
        populationButtons.InitiateButtons( DecreasePopulation, IncreasePopulation );

        speedButtons.DefaultText = "Speed x";
        speedButtons.Value = 1;
        speedButtons.InitiateButtons( DecreaseSpeed, IncreaseSpeed );
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();
    }

    private void OnPauseButtonClicked()
    {
        if ( _isPaused )
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        _isPaused = !_isPaused;
        pauseObject.SetActive( _isPaused );
    }

    private void DecreasePopulation()
    {
        if ( populationButtons.Value > 1 )
            populationButtons.Value--;

        _maxCirclesOnScreen = (int)populationButtons.Value;
    }

    private void IncreasePopulation()
    {
        if ( populationButtons.Value < 4 )
            populationButtons.Value++;

        _maxCirclesOnScreen = (int)populationButtons.Value;
    }

    private void DecreaseSpeed()
    {
        // [0.1..0.5] - 0.1, [0.5..1.0] - 0.5, [1.0..1.5] - 0.1, [1.5..5.0] - 0.5
        if ( ( speedButtons.Value > 0.11f && speedButtons.Value <= 0.5f )
            || ( speedButtons.Value > 1.0f && speedButtons.Value <= 1.5f ) )
        {
            speedButtons.Value -= 0.1f;
        }
        else
        if ( ( speedButtons.Value > 0.5f && speedButtons.Value <= 1.0f )
            || ( speedButtons.Value > 1.5f && speedButtons.Value <= 5.0f ) )
        {
            speedButtons.Value -= 0.5f;
        }

        _speedRatio = speedButtons.Value;
    }

    private void IncreaseSpeed()
    {
        // [0.1..0.5] - 0.1, [0.5..1.0] - 0.5, [1.0..1.5] - 0.1, [1.5..5.0] - 0.5
        if ( ( speedButtons.Value >= 0.1f && speedButtons.Value < 0.5f )
            || ( speedButtons.Value >= 1.0f && speedButtons.Value < 1.5f ) )
        {
            speedButtons.Value += 0.1f;
        }
        else
        if ( ( speedButtons.Value >= 0.5f && speedButtons.Value < 1.0f )
            || ( speedButtons.Value >= 1.5f && speedButtons.Value < 5.0f ) )
        {
            speedButtons.Value += 0.5f;
        }

        _speedRatio = speedButtons.Value;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if ( spawnTransform.childCount < _maxCirclesOnScreen && _time > _spawnCooldown )
        {
            CircleObject circle = Instantiate( circleObject, spawnTransform, false ).GetComponent<CircleObject>();
            CircleDifficulty difficulty = (CircleDifficulty)( (int)System.Math.Ceiling( Random.Range( 0.001f, 3.0f ) ) );

            circle.transform.localPosition = GetSpawnPoint( circle );
            circle.Initiate( scoreObject.AddPoints, difficulty, _speedRatio );

            _time = 0.0f;
        }
    }

    private Vector3 GetSpawnPoint( CircleObject circle )
    {
        float xSpawnPoint = 0.0f;
        float ySpawnPoint = 0.0f;
        Vector3 spawnPoint = Vector3.zero;

        do
        {
            xSpawnPoint = Random.Range( -1.0f * spawnTransform.rect.width / 2, spawnTransform.rect.width / 2);
            ySpawnPoint = Random.Range( -1.0f * spawnTransform.rect.height / 2, spawnTransform.rect.height / 2);
            spawnPoint = new Vector3( xSpawnPoint, ySpawnPoint, 0.0f );
        }
        while ( !IsSpawnPointAvailable( spawnPoint, circle ) );

        return spawnPoint;
    }

    private bool IsSpawnPointAvailable( Vector3 spawnPoint, CircleObject circle )
    {
        bool result = true;

        for ( int i = 0; i < spawnTransform.childCount - 1; i++ )
        {
            CircleObject circle0 = spawnTransform.GetChild( i ).GetComponent<CircleObject>();
            float distance = ( circle0.transform.localPosition - spawnPoint ).magnitude;
            float minDixtance = GetCircleRadius( circle ) + GetCircleRadius( circle0 );

            result = result && ( distance > minDixtance );
        }

        return result;
    }

    private float GetCircleRadius( CircleObject circle )
    {
        return System.Math.Max( circle.rectTransform.sizeDelta.x, circle.rectTransform.sizeDelta.y );
    }
}
