using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]


    [SerializeField] public float turnSpeed = 10f;
    [SerializeField] public float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField] public float visionRange = 18f;
    [SerializeField] public float fireRange = 15f;
    [SerializeField] public Transform firePoint1;
    [SerializeField] public Transform firePoint2;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float fixedDelay = 0.1f;
    [SerializeField] public GameObject fireEffectPrefab;

    [Header("Unity Setup Fields")]
    [SerializeField] public Transform partToRotate;
    [SerializeField] public Enemy target;   
    [SerializeField] public string enemyTag = "Enemy";
    GameObject nearestEnemy = null;
    // Variable to store the nearest enemy
    private bool useFirstFirePoint = true;
    // Flag to determine which fire point to use
    float shortestDistance = Mathf.Infinity;
    // Variable to store the shortest distance to an enemy


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        // Call UpdateTarget every 0.5 seconds to check for enemies in range
    }
    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // Get all enemies with the specified tag
        shortestDistance = Mathf.Infinity;
        // Variable to store the shortest distance to an enemy
      

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

        if (nearestEnemy != null && shortestDistance <= visionRange)
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
        if(fireCountdown <= 0 && shortestDistance <= fireRange)
        {
            Shoot();
            // If the countdown has reached zero, shoot a bullet
            fireCountdown = 1f / fireRate;
            // Reset the countdown for the next shot based on the fire rate
        }
        fireCountdown -= Time.deltaTime;
        // Decrease the countdown timer by the time since the last frame

    }

    void Shoot()
    {

        Transform firepoint = useFirstFirePoint ? firePoint1 : firePoint2;
        // Determine which fire point to use based on the flag
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        // Instantiate a new bullet at the fire point with the same rotation as the turret
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        // Create a new bullet instance and get its Bullet component
        if (fireEffectPrefab != null)
        // Check if the fire effect prefab is assigned
        {
            Instantiate(fireEffectPrefab, firepoint.position, firepoint.rotation);
            // Instantiate the fire effect prefab at the fire point
        }
        if (firePoint2 == null)
        {
            firePoint2 = firePoint1;
            // If the second fire point is not set, use the first fire point
        }
        useFirstFirePoint = !useFirstFirePoint;
        // Toggle the fire point for the next shot

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            // If the bullet component is found, set the target for the bullet to seek
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
        // Draw a red wire sphere in the editor to visualize the turret's range
    }
}
