using UnityEngine;
using UnityEngine.EventSystems;

/*
* This script has the purpose of handling the nodes and their abbility to check if they already have a turret built on them
*
* This script's objectives:
*   - Change node's colors according to if the player has money or not and if a turret is selected or not;
*   - Get the turret build position;
*   - Allow the node to know there is/isn't a turret already on it
*
* Works in close relationship with the build manager script (BuildManagerScript.cs)
*
* Used by GameObjects: Nodes' children
*/

public class NodeScript : MonoBehaviour
{   

    // Public variables
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    
    [Header("Optional")]
    public GameObject turret;

    // Private variables
    private Renderer rend;
    private Color startColor;
    BuildManagerScript buildManagerScript;

    // Initiates the object to get the renderer to handle colors, start color and instantiate the build manager
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManagerScript = BuildManagerScript.instance;
    }
    
    // Handles the event of the player pressing the left mouse button on a node
    void OnMouseDown()
    {
        // If the UI (or anything else) is in front of the node, color doesn't change
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        // Checks if there is a turret currently selected
        if(!buildManagerScript.CanBuild)
        {
            return;
        }
        // Checks if there is already a turret built on this node
        if(turret != null)
        {
            Debug.Log("Already has a turret built!"); // TODO
            return;
        }

        // If the node passes the previous checks, it means a turret can be built on this by the build manager
        buildManagerScript.BuildTurretOn(this);
    }

    // Handles the events when the player hovers the mouse over a node
    void OnMouseEnter()
    {
        // If the UI (or anything else) is in front of the node, nothing happens
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Check if a turret is already built on this node
        if(!buildManagerScript.CanBuild)
        {
            return;
        }

        // Check if the player has enough money to build the turrent, if he does, change to gray, if not, change to red
        if(buildManagerScript.HasMoney)
        {
            rend.material.color = hoverColor;   
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

        
    }

    // Handles the event where the player exits the mouse off of the node
    void OnMouseExit()
    {
        // If nothing can be built on the node, the initial color didn't change, so it doesn't need to be changed again
        if(!buildManagerScript.CanBuild)
        {
            return;
        }
        // Change color back to the original starting color when the mouse exits the node
        rend.material.color = startColor;
    }

    // Returns the position where the turret will be built, i. e. the node's position plus y (position offset)
    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

}
