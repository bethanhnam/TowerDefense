using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] float enemiesPerSecond = 0.5f;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy = new UnityEvent();
    int currentWave = 1;
    float timeSinceLastSpawn;
    int enemiesAlive;
    int enemiesLeftToSpawn;
    bool isspawning = false;
    private float eps; // enemies per second
    int level = 0;
	private void Awake()
	{
        OnEnemyDestroy.AddListener(EnemyDestroyed);
	}
	private void Start()
	{
        StartCoroutine(StartWave());
	}
	private void Update()
	{
        if (!isspawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn>=1f/eps && enemiesLeftToSpawn >0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
        }
        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
	}

	private void EndWave()
	{
        isspawning = false;
        timeSinceLastSpawn = 0;
        currentWave++;
		StartCoroutine(StartWave());
	}

	public void EnemyDestroyed() {
        enemiesAlive--;
    }
	private void SpawnEnemy()
	{
        int index = (int)UnityEngine.Random.Range((float)0, (float)enemyPrefabs.Length);
		GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity);
	}

	private IEnumerator StartWave()
	{
        yield return new WaitForSeconds(timeBetweenWaves);
        level++;
		isspawning = true;
		enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
	}
	private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
	private float EnemiesPerSecond()
	{
		return Mathf.Clamp((enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor)),0,enemiesPerSecondCap);
	}
}
