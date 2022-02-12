using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

public class SpawnSystem : MonoBehaviour
{
    public GameObject circleObject;
    public RectTransform spawnTransform;
    public ScoreObject scoreObject;

    private int _maxCirclesOnScreen = 2;
    private float _spawnCooldown = 0.2f;
    private float _time = 0.0f;



    private void Update()
    {
        _time += Time.deltaTime;

        if ( spawnTransform.childCount < _maxCirclesOnScreen && _time > _spawnCooldown )
        {
            CircleObject circle = Instantiate( circleObject, spawnTransform, false ).GetComponent<CircleObject>();
            CircleDifficulty difficulty = (CircleDifficulty)( (int)System.Math.Ceiling( Random.Range( 0.001f, 3.0f ) ) );

            circle.transform.localPosition = GetSpawnPoint( circle );
            circle.Initiate( scoreObject.AddPoints, difficulty );

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
