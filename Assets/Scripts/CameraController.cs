using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f; 
    // Speed of camera panning
    public float scrollSpeed = 10f;
    // Speed of camera zooming
    public float minY = 10f;
    // Minimum height of the camera
    public float maxY = 100f;
    // Maximum height of the camera
    public float panBorderThickness = 10f;
    // Thickness of the screen border for panning


    // Update is called once per frame
    void Update()
    {
                if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            // Move camera forward
        }
                if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            // Move camera back
        }
                if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            // Move camera right
        }
                if(Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            // Move camera left
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Get the scroll input
        Vector3 pos = transform.position;
        // Get the current position of the camera
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        // Adjust the height of the camera based on scroll input
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        // Clamp the height of the camera to the min and max values
        transform.position = pos;
        // Set the new position of the camera
        
    }
}
