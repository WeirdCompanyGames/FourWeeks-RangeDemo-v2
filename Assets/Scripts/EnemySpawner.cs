using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private GameObject newEnemy;
    private SpriteRenderer rend;
    private int randomSpawnZone;
    private float randomXposition, randomYposition;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 0, 2f);
    }

    private void SpawnNewEnemy()
    {
        randomSpawnZone = Random.Range(0, 4);

        switch (randomSpawnZone)
        {
            case 0:
                randomXposition = Random.Range(-11f, -10f);
                randomYposition = Random.Range(-8f, -8f);
                break;
            case 1:
                randomXposition = Random.Range(-10f, 10f);
                randomYposition = Random.Range(-7f, -8f);
                break;
            case 2:
                randomXposition = Random.Range(10f, 11f);
                randomYposition = Random.Range(-8f, 8f);
                break;
            case 3:
                randomXposition = Random.Range(-10f, 10f);
                randomYposition = Random.Range(7f, 8f);
                break;
        }

        spawnPosition = new Vector3(randomXposition, randomYposition, 0f);
        newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        rend = newEnemy.GetComponent<SpriteRenderer>();
        rend.color = new Color(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3), 1f);
    }
}