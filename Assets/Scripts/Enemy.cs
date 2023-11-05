using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;



public class Enemy : GameBehaviour
{
    public static event Action<GameObject> OnTargetHit = null;
    public static event Action<GameObject> OnTargetDie = null;



    public PatrolType myPatrol;
    public Difficulty difficulty;

    float baseSpeed = 10f;
    float speed;
    float moveDistance = 1000;

    public string myName;

    private int baseHealth = 100;
    private int health;
    private int maxHealth;
    public int scoreBonus;



    [Header("AI")]
    public TargetType myType;   //Types Wolf, Rabbit, Bear, Deer
    public Transform moveToPos; //Needed for all patrol movement    
    Transform startPos;         //Needed for loop patrol movement
    Transform endPos;           //Needed for loop patrol movement
    bool reverse;               //Needed for loop patrol movement
    int patrolPoint = 0;        //Needed for linear patrol movement


    void Start()
    {
        SetName(_EM.GetEnemyName());

        //Health and speed variables
        switch (myType)
        {
            case TargetType.Wolf:
                speed = baseSpeed;
                health = maxHealth = baseHealth;
                scoreBonus = 100;
                myPatrol = PatrolType.Linear;
                break;
            case TargetType.Rabbit:
                speed = baseSpeed * 2;
                health = maxHealth = baseHealth * 2;
                scoreBonus = 150;
                myPatrol = PatrolType.Random;
                break;
            case TargetType.Bear:
                speed = baseSpeed / 2;
                health = maxHealth = baseHealth / 2;
                scoreBonus = 200;
                myPatrol = PatrolType.Loop;
                break;
            case TargetType.Deer:
                speed = baseSpeed / 2;
                health = maxHealth = baseHealth / 2;
                scoreBonus = 200;
                myPatrol = PatrolType.Loop;
                break;
        }

        SetupAI();
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
        StartCoroutine(Move());
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            StopAllCoroutines();

        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit(20);
        }
    }

    public void SetName(string _name)
    {
        name = _name;

    }

    public void Hit(int _damage)
    {
        health = health -= _damage;
        ScaleObject(this.gameObject, transform.localScale * 1.05f);
        _GM.AddScore(scoreBonus);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            OnTargetHit?.Invoke(this.gameObject);
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        OnTargetDie?.Invoke(this.gameObject);
    }

    IEnumerator Move()
    {
        switch (myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }



        transform.LookAt(moveToPos);
        while(Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * speed);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Move());
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Hit(collision.gameObject.GetComponent<Projectile>().damage);
            Destroy(collision.gameObject);
        }
    }
}


