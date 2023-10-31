using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [Header("Rigidbody Projectiles")]
    public GameObject projectileBall;
    public float projectileSpeed = 1000f;
    public GameObject firingEffect;
    public GameObject hittingEffect;

    private void Start()
    {
        Destroy(projectileBall, 5);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRigidBody();
            firingEffect.SetActive(true);
            StartCoroutine(FireEffect());

        }
            

    }

    IEnumerator FireEffect()
    {
        yield return new WaitForSeconds(1f);
        firingEffect.SetActive(false);
    }

    IEnumerator HitEffect()
    {
        yield return new WaitForSeconds(4f);
        hittingEffect.SetActive(true);
    }

    void FireRigidBody()
    {
        //Create a refernce to hold our instantiated object
        GameObject projectileInstance;
        //Instantiate our projectile at this objects position and rotation
        projectileInstance = Instantiate(projectileBall, transform.position, transform.rotation);
        //Add force to the projectile
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

        StartCoroutine(HitEffect());
    }
}
