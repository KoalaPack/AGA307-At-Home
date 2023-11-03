using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int taregtHealth = 100;
    public int damageBase = 20;
    private int damageCal;


    void Start()
    {
        damageBase = 20;
        damageCal = damageCal + damageBase;
        taregtHealth = 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            damageBase = 20;
            damageCal = damageBase * 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            damageBase = 20;
            damageCal = damageBase * 2;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            damageBase = 20;
            damageCal = damageBase * 3;
        }
    }

    public void TargetHealthChecker()
    {
        taregtHealth = taregtHealth - damageCal;
        //Destroy the Target after 1 second
        if (taregtHealth == 100)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        
        else if (taregtHealth == 80)
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        else if (taregtHealth == 60)
            gameObject.GetComponent<Renderer>().material.color = Color.green;

        else if (taregtHealth == 40)
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;

        else if (taregtHealth == 20)
            gameObject.GetComponent<Renderer>().material.color = Color.blue;

        else if (taregtHealth <= 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
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
}
