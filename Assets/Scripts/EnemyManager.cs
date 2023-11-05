using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType{Wolf, Rabbit, Bear, Deer}

public enum PatrolType
{
    Linear,
    Random,
    Loop
}
public class EnemyManager : Singleton<EnemyManager>
{

    public Transform[] spawnPoints;
    public string[] enemyNames;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;

    public string killCondition = "Two";

    private void Start()
    {
        StartCoroutine(SpawnAfterTime());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();

        if (Input.GetKeyDown(KeyCode.K))
            KillEnemy(enemies[0]);

        if (Input.GetKeyDown(KeyCode.L))
            KillAllEnemies();

        if (Input.GetKeyDown(KeyCode.P))
            KillSpecificEnemies(killCondition);
    }

    /// <summary>
    /// Spawns an enemy every 2 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnAfterTime()
    {
        for (int i = 0; i <= enemyTypes.Length - 1; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemy.name = enemyNames[i];
            enemies.Add(enemy);
            yield return new WaitForSeconds(2);


        }
    }
    /// <summary>
    /// Spawns an enemy at every spawn point
    /// </summary>
    /// 

    void SpawnEnemies()
    {
        for (int i = 0; i <= enemyTypes.Length - 1; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemy.name = enemyNames[i];
            enemies.Add(enemy);
        }
    }

    public void SpawnAtRandom()
    {

        int rnd = Random.Range(0, enemyTypes.Length);
        int rnd1 = Random.Range(0, enemyTypes.Length);
        GameObject enemy = Instantiate(enemyTypes[rnd1], spawnPoints[rnd].position, spawnPoints[rnd].rotation);
        enemy.name = enemyNames[rnd];
        enemies.Add(enemy);
        ShowtEnemyCount();

    }

    /// <summary>
    /// Shows the amount of enemies in the stage
    /// </summary>
    void ShowtEnemyCount()
    {
        _UI.UpdateEnemyCount(enemies.Count);
    }

    public string GetEnemyName()
    {
        return enemyNames[Random.Range(0, enemyNames.Length)];
    }

    /// <summary>
    /// Kills a specific enemy
    /// </summary>
    /// <param name="_enemy">The enemy we want to kill</param>

    public void KillEnemy(GameObject _enemy)
    {
        if(enemies.Count == 0)
            return;
        Destroy(_enemy);
        enemies.Remove(_enemy);
        ShowtEnemyCount();
    }

    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;
        for(int i = enemies.Count-1; i >= 0; i++)
        {
            KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Kills Specific enemies
    /// </summary>
    /// <param name="_condition">The confition of the enemy we want to kill</param>
    void KillSpecificEnemies(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Get a random spawn point
    /// </summary>
    /// <returns>A random spawn point</returns>
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }


    private void OnEnable()
    {
        Enemy.OnTargetDie += KillEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnTargetDie -= KillEnemy;
    }



    void Examples()
    {
        for (int i = 0; i <= 1000; i++)
        {
            print(i);
        }


        GameObject first = Instantiate(enemyTypes[0], spawnPoints[0].position, spawnPoints[0].rotation);
        first.name = enemyNames[0];

        int lastEnemy = enemyTypes.Length - 1;
        GameObject last = Instantiate(enemyTypes[lastEnemy], spawnPoints[lastEnemy].position, spawnPoints[lastEnemy].rotation);
        last.name = enemyNames[lastEnemy];

        //Creating a loop within a loop
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(wall, new Vector3(i, 0, 0), transform.rotation);
            }
        }
    }
}
