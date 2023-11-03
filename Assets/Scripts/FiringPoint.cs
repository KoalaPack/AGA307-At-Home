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

    public List<GameObject> projectileTypes; // Renamed to match the variable name

    private int currentProjectileTypeIndex;

    private void Start()
    {
        Destroy(projectileBall, 5);
        SwitchToGameObject(0);

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRigidBody();
            firingEffect.SetActive(true);
            StartCoroutine(FireEffect());
        }

        // Check for number keys 1, 2, and 3 to switch projectile types
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToGameObject(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToGameObject(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToGameObject(2);
        }


    }

    private void SwitchToGameObject(int newIndex)
    {
        // Ensure the newIndex is valid
        if (newIndex < 0 || newIndex >= projectileTypes.Count)
        {
            Debug.LogWarning("Invalid game object index.");
            return;
        }

        // Deactivate the current game object
        ToggleGameObject(currentProjectileTypeIndex, false);

        // Update the current index
        currentProjectileTypeIndex = newIndex;

        // Activate the new game object
        ToggleGameObject(currentProjectileTypeIndex, true);
    }

    private void ToggleGameObject(int index, bool active = true)
    {
        if (index >= 0 && index < projectileTypes.Count)
        {
            projectileTypes[index].SetActive(active);
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
        // Create a reference to hold our instantiated object
        GameObject projectileInstance;
        // Instantiate our projectile at this object's position and rotation
        projectileInstance = Instantiate(projectileTypes[currentProjectileTypeIndex], transform.position, transform.rotation);
        // Add force to the projectile
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

        StartCoroutine(HitEffect());
    }
}
