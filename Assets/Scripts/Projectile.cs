using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    public int projectileDestoryTime = 3;
    

    void Start()
    {
        Destroy(this.gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if we hit the object tagged Target
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
        }

    }
}
