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
* Works in close relationship with the build manager, turretBlueprint and NodeUI scripts (BuildManagerScript.cs, TurretBlueprintScript.cs and NodeUIScript.cs)
*
* Used by GameObjects: Nodes' children
*/

public class NodeScript : MonoBehaviour
{   

    // Public variables
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprintScript turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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
        
        // Checks if there is already a turret built on this node
        if(turret != null)
        {
            // If we click the node when it already has a turret there, we select the node to open the Node Turret UI
            buildManagerScript.SelectNode(this);
            return;
        }

        // Checks if there is a turret currently selected
        if(!buildManagerScript.CanBuild)
        {
            return;
        }

        // If the node passes the previous checks, it means a turret can be built on this
        BuildTurret(buildManagerScript.GetTurretToBuild());
    }

    // Builds a turret on this node
    void BuildTurret(TurretBlueprintScript blueprint)
    {
        // Checks whether the player has enough money for the current selected turret
        if(PlayerStatsScript.Money < blueprint.cost)
        {
            Debug.Log("Not enough money!"); // TODO
            return;
        }

        // Subtract money from player relative to turret cost, build it and play build effect
        PlayerStatsScript.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        // Set this for later upgrading the turret
        turretBlueprint = blueprint;

        GameObject buildEffect = (GameObject)Instantiate(buildManagerScript.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        Debug.Log("Turret built. Money left: " + PlayerStatsScript.Money); // TODO
    }

    // Upgrades the selected turret
    public void UpgradeTurret()
    {
        // Checks whether the player has enough money for the current selected turret
        if(PlayerStatsScript.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!"); // TODO
            return;
        }

        // Subtract money from player relative to turret cost, build it and play build effect
        PlayerStatsScript.Money -= turretBlueprint.upgradeCost;

        // Destroys the base turret
        Destroy(turret);

        // Replaces it with an upgraded version of it
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject)Instantiate(buildManagerScript.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        // Sets this node's status to turret is present and upgraded
        isUpgraded = true;

        Debug.Log("Turret upgraded! Money left: " + PlayerStatsScript.Money); // TODO
    }

    // Sell the turret built on this node
    public void SellTurret()
    {
        // Turret value is based on its base cost plus half the upgrade cost if it has been upgraded
        int sellValue = turretBlueprint.GetSellValue();
        if(isUpgraded)
        {
            sellValue += (turretBlueprint.upgradeCost / 2);
        }
        PlayerStatsScript.Money += sellValue;
        sellValue = 0;

        // Sell effect
        GameObject sellEffect = (GameObject)Instantiate(buildManagerScript.sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);

        // Destroy the turret
        Destroy(turret);
        turretBlueprint = null;
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
        /* Piece of code removed because when NodeUI was opened, we couldn't build turret there, so it kept the node selected
        // If nothing can be built on the node, the initial color didn't change, so it doesn't need to be changed again
        if(!buildManagerScript.CanBuild)
        {
            return;
        }*/

        // Change color back to the original starting color when the mouse exits the node
        rend.material.color = startColor;
    }

    // Returns the position where the turret will be built, i. e. the node's position plus y (position offset)
    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

}
