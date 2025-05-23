using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    // Reference to the standard turret blueprint
    public TurretBlueprint missileLauncher;
    // Reference to the missile launcher blueprint
    BuildManager buildManager;
    // Reference to the BuildManager instance
    void Start()
    {
        buildManager = BuildManager.instance;
        // Get the instance of BuildManager
    }
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        // This method is called when the player select a standard turret
        buildManager.SelectTurretToBuild(standardTurret);
        // Set the turret prefab to be built to the standard turret prefab
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        // This method is called when the player select a missile launcher
        buildManager.SelectTurretToBuild(missileLauncher);
        // Set the turret prefab to be built to the missile launcher prefab
    }
}
