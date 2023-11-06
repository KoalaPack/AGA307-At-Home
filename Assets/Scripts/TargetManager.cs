
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { Wolf, Rabbit, Bear }

public enum PatrolType { Linear, Random, Loop }

public class TargetManager : Singleton<TargetManager>
{

    public Transform[] spawnPoints;
    public string[] targetNames;
    public GameObject[] targetTypes;

    public List<GameObject> targets;

    public string killCondition = "Two";


    public void Start()
    {
        StartCoroutine(SpawnAfterTime());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();

        if (Input.GetKeyDown(KeyCode.K))
            KillTarget(targets[0]);

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
        for (int i = 0; i <= targetTypes.Length - 1; i++)
        {
            int rnd = Random.Range(0, targetTypes.Length);
            GameObject target = Instantiate(targetTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            target.name = targetNames[i];
            targets.Add(target);
            SpawnAtRandom();
            yield return new WaitForSeconds(10);


        }
    }
    /// <summary>
    /// Spawns an enemy at every spawn point
    /// </summary>
    /// 

    void SpawnEnemies()
    {
        for (int i = 0; i <= targetTypes.Length - 1; i++)
        {
            int rnd = Random.Range(0, targetTypes.Length);
            GameObject target = Instantiate(targetTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            target.name = targetNames[i];
            targets.Add(target);
        }
    }

    public void SpawnAtRandom()
    {

        int rnd = Random.Range(0, targetTypes.Length);
        int rnd1 = Random.Range(0, targetTypes.Length);
        GameObject target = Instantiate(targetTypes[rnd1], spawnPoints[rnd].position, spawnPoints[rnd].rotation);
        target.name = targetNames[rnd];
        targets.Add(target);
        ShowTargetCount();


    }

    /// <summary>
    /// Shows the amount of enemies in the stage
    /// </summary>
    void ShowTargetCount()
    {
        _UI.UpdateTargetCount(targets.Count);
    }

    public string GetEnemyName()
    {
        return targetNames[Random.Range(0, targetNames.Length)];
    }

    /// <summary>
    /// Kills a specific enemy
    /// </summary>
    /// <param name="_target">The enemy we want to kill</param>

    public void KillTarget(GameObject _target)
    {
        if (targets.Count == 0)
            return;
        Destroy(_target);
        targets.Remove(_target);
        ShowTargetCount();
    }

    void KillAllEnemies()
    {
        if (targets.Count == 0)
            return;
        for (int i = targets.Count - 1; i >= 0; i++)
        {
            KillTarget(targets[i]);
        }
    }

    /// <summary>
    /// Kills Specific enemies
    /// </summary>
    /// <param name="_condition">The confition of the enemy we want to kill</param>
    void KillSpecificEnemies(string _condition)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].name.Contains(_condition))
                KillTarget(targets[i]);
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


    public void OnEnable()
    {
        Target.OnTargetDie += KillTarget;
    }

    public void OnDisable()
    {
        Target.OnTargetDie -= KillTarget;
    }



    void Examples()
    {
        for (int i = 0; i <= 1000; i++)
        {
            print(i);
        }


        GameObject first = Instantiate(targetTypes[0], spawnPoints[0].position, spawnPoints[0].rotation);
        first.name = targetNames[0];

        int lastEnemy = targetTypes.Length - 1;
        GameObject last = Instantiate(targetTypes[lastEnemy], spawnPoints[lastEnemy].position, spawnPoints[lastEnemy].rotation);
        last.name = targetNames[lastEnemy];

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
