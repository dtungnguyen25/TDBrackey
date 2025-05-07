using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    // Reference to the impact effect prefab to be instantiated on hit

    public void Seek (Transform _target)
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
            Destroy(gameObject);
            // If no target is set, destroy the bullet
            return;
        }        

        Vector3 direction = target.position - transform.position;
        // Calculate the direction to the target
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

    void HitTarget()
    {
        Debug.Log("Hit " + target.name);
        // Log the name of the target hit
        Instantiate(impactEffect, transform.position, transform.rotation);
        // Instantiate the impact effect at the bullet's position and rotation

        Destroy(gameObject);
        // Destroy the bullet when it hits the target
        // You can also add code here to deal damage to the target or play a hit effect
    }
}
