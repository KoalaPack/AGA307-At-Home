using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Projectile projectileScript;
    public int taregtHealth = 100;
    public int ProjectileDamage = 20;
    
    void Start()
    {
        taregtHealth = 100;
        projectileScript.damage = ProjectileDamage;
    }

    public void TargetHealthChecker()
    {
        taregtHealth = taregtHealth - ProjectileDamage;
        //Destroy the Target after 1 second
        if (taregtHealth == 100)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        
        if (taregtHealth == 80)
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        if (taregtHealth == 60)
            gameObject.GetComponent<Renderer>().material.color = Color.green;

        if (taregtHealth == 40)
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;

        if (taregtHealth == 20)
            gameObject.GetComponent<Renderer>().material.color = Color.blue;

        if (taregtHealth <= 0)
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
