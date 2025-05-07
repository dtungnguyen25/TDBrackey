using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
        // Initialize the target to the first waypoint
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        // Move towards the target waypoint

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        // Check if the enemy is close enough to the target waypoint
        {
            GetNextWaypoint();
        }
        // If the enemy is close enough to the target waypoint, get the next waypoint
    
        void GetNextWaypoint()
        {
            if (waypointIndex >= Waypoints.points.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            // If the enemy has reached the last waypoint, destroy it
            waypointIndex++;
            // Increment the waypoint index
            target = Waypoints.points[waypointIndex];
            // Set the target to the next waypoint
        }

    }
}
