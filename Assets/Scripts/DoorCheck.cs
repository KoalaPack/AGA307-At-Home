using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{

    //[Header("Raycast Projectiles")]
    //public GameObject hitMarker;
    //public LineRenderer laser;


    //void Update()
    //{
    //    if (Input.GetButtonDown("E"))
    //        FireRigidBody();

    //}


    //void FireRigidBody()
    //{
    //    //Create a refernce to hold our instantiated object
    //    GameObject projectileInstance;
    //    //Instantiate our projectile at this objects position and rotation
    //    projectileInstance = Instantiate(SpikeBall, transform.position, transform.rotation);
    //    //Add force to the projectile
    //    projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    //}

    //void FireRaycast()
    //{
    //    //Create the ray
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    //Create a reference to hold the info on what we hit
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        //Debug.Log("Ray hit " + hit.collider.name + " at point " + hit.point + " which was " + hit.distance + " units away");
    //        laser.SetPosition(0, transform.position);
    //        laser.SetPosition(1, hit.point);
    //        StopAllCoroutines();
    //        StartCoroutine(StopLaser());


    //        GameObject particles = Instantiate(EnergyExplosion, hit.point, hit.transform.rotation);
    //        Destroy(particles, 0.5f);

    //        if (hit.collider.CompareTag("Target"))
    //        {
    //            Destroy(hit.collider.gameObject);
    //        }
    //    }
    //}

    //IEnumerator StopLaser()
    //{
    //    laser.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(0.4f);
    //    laser.gameObject.SetActive(false);
    //}
}
