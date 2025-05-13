using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    // Reference to the impact effect prefab to be instantiated on hit
    public float explosionRadius = 0f;
    // Radius of the explosion effect
    public float delayDestroy = 1f;
    // Delay before destroying the bullet after impact
    private bool destroyed;
    // Time to wait before destroying the bullet


    public void Seek(Transform _target)
    {
        target = _target;
        // Set the target to the specified transform    
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (!destroyed)
                StartCoroutine(DestroyBullet());
            // If the target is null, start the coroutine to destroy the bullet
        }

        if (!destroyed)
        {

            Vector3 direction = target.position - transform.position;
            // Calculate the direction to the target
            transform.forward = direction;
            // Rotate the bullet to face the target
            float distanceThisFrame = speed * Time.deltaTime;
            // Calculate the distance to move this frame
            if (direction.magnitude <= distanceThisFrame)
            // Check if the bullet is close enough to the target
            {
                HitTarget();
                return;
            }
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            // Move the bullet towards the target in the direction of the target
        }
    }
    IEnumerator DestroyBullet()
    {
        destroyed = true;
        // If the target is null, set destroyed to true
        yield return new WaitForSeconds(delayDestroy);
        // Wait for the specified delay before destroying the bullet
        Destroy(gameObject);
        // Destroy the bullet game object
    }

    void HitTarget()
    {
        Debug.Log("Hit " + target.name);
        if (!destroyed)
            StartCoroutine(DestroyBullet());
        // If the bullet has not been destroyed, start the coroutine to destroy it
        if (impactEffect)       
        // Check if the impact effect prefab is assigned
        {
            // Instantiate the impact effect at the bullet's position and rotation
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        if (explosionRadius > 0f)
        {
            Explode();
            // If there is an explosion radius, call the Explode method
        }
        else
        {
            Damage(target);
            // If there is no explosion radius, apply damage to the target
        }
        // Fill in the damage function here
    }
    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        // Get all colliders within the explosion radius
        for (int i = 0; i < colliders.Length; i++)
        {
            Damage(colliders[i].transform);
            // Loop through all colliders and apply damage to each one
        }
    }

    void Damage(Transform enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        // Check if the enemy has the "Enemy" tag
        {
            Destroy(enemy.gameObject);
            // Destroy the bullet game object
        }
        
    }
}
