using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    // Singleton instance of BuildManager
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            // If there is already an instance of BuildManager, log an error and exit the method
            Destroy(gameObject);
            // Destroy the duplicate instance
            return;
        }
        instance = this;
        // Set the instance to this BuildManager
    }
    
    public GameObject standardTurretPrefab;
    // Prefab for the standard turret
    public GameObject missileLauncherPrefab;
    // Prefab for the missile launcher


    private GameObject turretToBuild;
    // The turret prefab that will be built

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
        // Return the turret prefab to be placed on the node
    }
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
        // Set the turret prefab to be built
    }
}
