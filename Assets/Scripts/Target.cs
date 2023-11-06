using System;
using System.Collections;
using UnityEngine;

public class Target : GameBehaviour
{
    public static event Action<GameObject> OnTargetHit = null;
    public static event Action<GameObject> OnTargetDie = null;

    public PatrolType myPatrol;
    public Difficulty difficulty;

    float baseSpeed = 5f;
    float speed;

    public string myName;

    public int scoreBonus;

    public int taregtHealth = 100;
    int maxHealth;
    public int damageBase = 20;
    private int damageCal;


    [Header("AI")]
    public TargetType myType;   //Types Wolf, Rabbit, Bear, Deer
    public Transform moveToPos; //Needed for all patrol movement    
    Transform startPos;         //Needed for loop patrol movement
    Transform endPos;           //Needed for loop patrol movement
    bool reverse;               //Needed for loop patrol movement
    int patrolPoint = 0;        //Needed for linear patrol movement

    public GameObject childObject;


    [Header("Random Scale")]
    float minScale = 0.4f;  // Minimum scale value
    float maxScale = 2f;  // Maximum scale value

    void Start()
    {
        maxHealth = 100;
        damageBase = 20;
        damageCal = damageCal + damageBase;

        switch (difficulty)
        {
            case Difficulty.Easy:
                taregtHealth = 100;
                break;
            case Difficulty.Medium:
                taregtHealth = 200;
                break;
            case Difficulty.Hard:
                taregtHealth = 300;
                break;
        }

        SetName(_TM.GetEnemyName());

        //Health and speed variables
        switch (myType)
        {
            case TargetType.Wolf:
                speed = baseSpeed;
                scoreBonus = 100;
                myPatrol = PatrolType.Random;
                break;
            case TargetType.Rabbit:
                speed = baseSpeed * 2;
                scoreBonus = 150;
                myPatrol = PatrolType.Random;
                break;
            case TargetType.Bear:
                speed = baseSpeed / 2;
                scoreBonus = 200;
                myPatrol = PatrolType.Loop;
                break;
        }

        SetupAI();
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _TM.GetRandomSpawnPoint();
        moveToPos = endPos;
        StartCoroutine(Move());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            damageBase = 20;
            damageCal = damageBase * 1;
            ScaleObject(this.gameObject, new Vector3(1, 1, 1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            damageBase = 20;
            damageCal = damageBase * 2;
            ScaleObject(this.gameObject, new Vector3(.6f, .6f, .6f));
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            damageBase = 20;
            damageCal = damageBase * 3;
            ScaleObject(this.gameObject, new Vector3(.3f, .3f, .3f));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Generate new random scale values within the specified range
            float randomScaleX = UnityEngine.Random.Range(minScale, maxScale);
            float randomScaleY = UnityEngine.Random.Range(minScale, maxScale);
            float randomScaleZ = UnityEngine.Random.Range(minScale, maxScale);

            Vector3 randomScale = new Vector3(randomScaleX, randomScaleY, randomScaleZ);

            // Apply the random scale to the game object
            ScaleObject(this.gameObject, randomScale);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
            StopAllCoroutines();

        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit();
        }

    }

    public void Hit()
    {
        taregtHealth = taregtHealth - 20;
    }

    public void TargetHealthChecker()
    {
        Renderer childRenderer = childObject.GetComponent<Renderer>();

        taregtHealth = taregtHealth - damageCal;
        if (taregtHealth == (maxHealth * 0.8))
        {
            childRenderer.material.color = Color.yellow;
            ScaleObject(this.gameObject, new Vector3(1.1f, 1.1f, 1.1f));
            speed = speed + 3f;
        }
        else if (taregtHealth == (maxHealth * 0.6))
        {
            childRenderer.material.color = Color.green;
            ScaleObject(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f));
            speed = speed + 4f;
        }
        else if (taregtHealth == (maxHealth * 0.4))
        {
            childRenderer.material.color = Color.cyan;
            ScaleObject(this.gameObject, new Vector3(1.3f, 1.3f, 1.3f));
            speed = speed + 5f;
        }
        else if (taregtHealth == (maxHealth * 0.2))
        {
            childRenderer.material.color = Color.blue;
            ScaleObject(this.gameObject, new Vector3(1.4f, 1.4f, 1.4f));
            speed = speed + 10f;
        }
        else if (taregtHealth <= 0)
        {
            _Time.AddTime();
            
            childRenderer.material.color = Color.gray;
            ScaleObject(this.gameObject, new Vector3(0.5f, 0.5f, 0.5f));
            Die();
            Destroy(this.gameObject, 1);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TargetHealthChecker();
        }
    }


    public void SetName(string _name)
    {
        name = _name;

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
                moveToPos = _TM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _TM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Random:
                moveToPos = _TM.GetRandomSpawnPoint();
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }



        transform.LookAt(moveToPos);
        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * speed);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Move());
    }
}
