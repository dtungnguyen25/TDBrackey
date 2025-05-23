using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor; 
    // Color to change to when the mouse hovers over the node
    public Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);
    // Offset to position the turret above the node

    [Header("Optional")]
    public GameObject turret;
    // Reference to the turret prefab that will be placed on this node
    private Color defaultColor;
    // Default color of the node
    private Renderer rend;
    // Renderer component to change the color of the node
    BuildManager buildManager;
    // Reference to the BuildManager instance

    void Start()
    // This method is called when the script instance is being loaded
    {
        rend = GetComponent<Renderer>();
        // Get the Renderer component attached to this GameObject
        defaultColor = rend.material.color;
        // Store the default color of the node
        buildManager = BuildManager.instance;
        // Get the instance of BuildManager
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
        // Return the position of the node with the offset
    }

    void OnMouseDown()
    // This method is called when the mouse button is pressed over the collider attached to this GameObject
    {
        if (!buildManager.CanBuild)
        // Check if there is a turret prefab selected to build
        return;
        // If no turret is selected, exit the method

        if (turret != null)
        {
            Debug.Log("Node is already occupied by a turret.");
            return;
            // If a turret is already placed, log a message and exit the method
        }

        buildManager.BuildTurretOn(this);
        // Call the BuildTurretOn method in BuildManager to place the turret on this node

    }
    void OnMouseEnter()
    // This method is called when the mouse pointer enters the collider attached to this GameObject
    {
        if (!buildManager.CanBuild)
        // Check if there is a turret prefab selected to build
        return;
        // If no turret is selected, exit the method
        rend.material.color = hoverColor;
        // Change the color of the node to the hover color when the mouse enters
    }

    void OnMouseExit()
    // This method is called when the mouse pointer exits the collider attached to this GameObject
    {
        rend.material.color = defaultColor;
        // Reset the color of the node to default when the mouse exits
    }
}
