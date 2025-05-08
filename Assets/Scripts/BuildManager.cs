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
            return;
        }
        instance = this;
        // Set the instance to this BuildManager
    }
    
    public GameObject standardTurretPrefab;
    // Prefab for the standard turret
    private GameObject turretToBuild;
    // The turret prefab that will be built

    void Start()
    {
        turretToBuild = standardTurretPrefab;
        // Set the turret to build to the standard turret prefab at the start of the game
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
        // Return the turret prefab to be placed on the node
    }

}
