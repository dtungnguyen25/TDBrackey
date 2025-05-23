using UnityEngine;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject dieEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        // Initialize the current health to the maximum health
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Decrease the current health by the specified amount
        if (currentHealth <= 0)
        // Check if the current health is less than or equal to zero
        {
            Instantiate(dieEffectPrefab, transform.position, transform.rotation);
            // Instantiate the die effect prefab at the object's position and rotation
            Die();
            // Call the Die method to handle the object's death
        }
    }
    void Die()
    {
        Destroy(gameObject);
        // Destroy the object
    }
}
