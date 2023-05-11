using UnityEngine;

/*
* This script allows for movement controls with mouse and WASD as well as zoom in/out with mouse wheel
*
* Uses the gameOver variable from GameManagerScript.cs to know when the game is over
*
* Used by GameObjects: MainCamera
*/

public class CameraControllerScript : MonoBehaviour
{

    // Public variables
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        // Disable camera controls if game is over
        if(GameManagerScript.gameOver)
        {
            // TODO change position back to initial
            this.enabled = false;
            return;
        }

        // WASD and mouse pan controls (Unrestricted TODO)
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Zoom (Mouse Wheel) controls: 
        //      - Get input 
        //      - Get current camera position 
        //      - Scroll in the y axis according to scroll speed and time 
        //      - Limit with clamp
        //      - Update position accordingly
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

}
