using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject meteorite;
    public float spawnRate = 6f;
    private float timer = 0f;

    public float minY = -4f, maxY = 4f;

    void Start()
    {
        meteorite = Resources.Load<GameObject>("meteorite");
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnMeteorite();
            timer = 0;
        }
    }

    void spawnMeteorite()
    {
        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(10, spawnY, 0);
        Quaternion spawnRotation = Quaternion.Euler(0, 0, -90);
        Instantiate(meteorite, spawnPosition, spawnRotation);
        // Debug.Log("Created Meteor! KABOOM!");
    }
}