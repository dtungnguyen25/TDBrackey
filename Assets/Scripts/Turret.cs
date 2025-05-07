using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]


    [SerializeField] public float turnSpeed = 10f;
    [SerializeField] public float fireRate = 1f;
    [SerializeField] private float fireCountdown = 0f;
    [SerializeField] public float range = 15f;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject bulletPrefab;

    [Header("Unity Setup Fields")]
    [SerializeField] public Transform partToRotate;
    [SerializeField] public Enemy target;   
    [SerializeField] public string enemyTag = "Enemy";
    [SerializeField] private 

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        // Call UpdateTarget every 0.5 seconds to check for enemies in range
    }
    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // Get all enemies with the specified tag
        float shortestDistance = Mathf.Infinity;
        // Variable to store the shortest distance to an enemy
        GameObject nearestEnemy = null;
        // Variable to store the nearest enemy

        foreach (GameObject enemy in enemies) // Loop through all enemies
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // Calculate the distance to the enemy

            if (distanceToEnemy < shortestDistance)
            // Check if the enemy is within range and closer than the current shortest distance
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                // Update the shortest distance and nearest enemy
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<Enemy>();
            // If a nearest enemy is found within range, set it as the target
        }
        else
        {
            target = null;
            // If no enemy is found within range, set the target to null
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) 
        return;
        // If there is no target, exit the update method
        Vector3 direction = target.transform.position - transform.position;
        // Calculate the direction to the target enemy
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Create a rotation that looks at the target enemy
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime*turnSpeed ).eulerAngles;
        // Convert the rotation to Euler angles
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        // Rotate the turret to face the target enemy
        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
            // Reset the countdown for the next shot based on the fire rate
        }
        fireCountdown -= Time.deltaTime;
        // Decrease the countdown timer by the time since the last frame

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Instantiate a new bullet at the fire point with the same rotation as the turret
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        // Create a new bullet instance and get its Bullet component

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            // If the bullet component is found, set the target for the bullet to seek
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        // Draw a red wire sphere in the editor to visualize the turret's range
    }
}
