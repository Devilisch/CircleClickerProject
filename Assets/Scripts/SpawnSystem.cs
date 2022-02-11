using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

public class SpawnSystem : MonoBehaviour
{
    public GameObject circleObject;
    public RectTransform spawnTransform;
    public ScoreObject scoreObject;

    private List<GameObject> spawnObjects = new List<GameObject>();



    private void Update()
    {
        if ( spawnTransform.childCount < 2 )
        {
            CircleObject circle = Instantiate( circleObject, spawnTransform, false ).GetComponent<CircleObject>();
            CircleStage stage = (CircleStage)( (int)System.Math.Ceiling( Random.Range( (float)CircleStage.B, (float)CircleStage.E ) ) );

            circle.transform.localPosition = GetSpawnPoint( circle );
            circle.Initiate( scoreObject.AddPoints, stage );
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

        // including extra dot in ( 0.0f, 0.0f ) point
        for ( int i = 0; i < spawnTransform.childCount; i++ )
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
