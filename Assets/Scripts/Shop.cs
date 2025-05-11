using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    // Reference to the BuildManager instance
    void Start()
    {
        buildManager = BuildManager.instance;
        // Get the instance of BuildManager
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        // This method is called when the player purchases a standard turret
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
        // Set the turret prefab to be built to the standard turret prefab
    }
    public void PurchaseMissileLauncher()
    {
        Debug.Log("Missile Launcher Purchased");
        // This method is called when the player purchases a missile launcher
        buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
        // Set the turret prefab to be built to the missile launcher prefab
    }
}
