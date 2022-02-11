using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;

public class SpawnSystem : MonoBehaviour
{
    public GameObject circleObject;
    public RectTransform spawnTransform;

    private List<GameObject> spawnObjects = new List<GameObject>();



    private void Update()
    {
        if ( spawnTransform.childCount < 2 )
        {
            float xSpawnPoint = Random.Range( -1.0f * spawnTransform.rect.width / 2, spawnTransform.rect.width / 2);
            float ySpawnPoint = Random.Range( -1.0f * spawnTransform.rect.height / 2, spawnTransform.rect.height / 2);
            Vector3 spawnPoint = new Vector3( xSpawnPoint, ySpawnPoint, 0.0f );
            CircleObject circle = Instantiate( circleObject, spawnTransform, false ).GetComponent<CircleObject>();

            circle.transform.localPosition = spawnPoint;
            circle.Initiate( (CircleStage)( (int)System.Math.Ceiling( Random.Range( (float)CircleStage.B, (float)CircleStage.E ) ) ) );
        }
    }
}
