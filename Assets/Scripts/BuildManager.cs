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


    private TurretBlueprint turretToBuild;
    // The turret prefab that will be built

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
            // Check if there is a turret prefab selected to build
        }
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build this turret!");
            // If the player does not have enough money, log a message and exit the method
            return;
        }

        PlayerStats.money -= turretToBuild.cost;
        // Deduct the cost of the turret from the player's money
        
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        // Instantiate the turret prefab at the node's position with no rotation
        node.turret = turret;
        // Set the turret reference in the node to the newly instantiated turret

        Debug.Log("Turret built! Money left: " + PlayerStats.money);
        // Log the remaining money after building the turret
    }

    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        // Set the turret prefab to be built to the selected turret prefab
    }
}
