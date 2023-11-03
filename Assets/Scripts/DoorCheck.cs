using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{

    [Header("Raycast Projectiles")]
    public LineRenderer laser;

    public GameObject hitSparks;


    void Update()
    {
        if (Input.GetButtonDown("E"))
            FireRaycast();

    }

    void FireRaycast()
    {
        //Create the ray
        Ray ray = new Ray(transform.position, transform.forward);
        //Create a reference to hold the info on what we hit
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log("Ray hit " + hit.collider.name + " at point " + hit.point + " which was " + hit.distance + " units away");
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            StopAllCoroutines();

            GameObject particles = Instantiate(hitSparks, hit.point, hit.transform.rotation);
            Destroy(particles, 0.5f);

            if (hit.collider.CompareTag("Door"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

}
