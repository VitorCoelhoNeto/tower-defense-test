using UnityEngine;

/*
* This script is treated as a singleton across the app 
* It serves the purpose of managing which turret is currently selected and the act of building a turret
*
* Works in close relationship with the node, node UI and shop scripts (NodeScript.cs, NodeUIScript.cs and ShopScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class BuildManagerScript : MonoBehaviour
{

    // Private variables
    private TurretBlueprintScript turretToBuild;
    private NodeScript selectedNode;

    // Public variables
    public static BuildManagerScript instance;
    public GameObject buildEffectPrefab;
    public GameObject sellEffectPrefab;
    public NodeUIScript nodeUIScript;
    public bool CanBuild {  get{  return turretToBuild != null; }  } // Check if there is a turret selected or not
    public bool HasMoney {  get{  return PlayerStatsScript.Money >= turretToBuild.cost; }  } // Check if player has money for the selected turret or not

    // Checks whether the game already has a build manager, if not, create it 
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error - more than one build manager!");
            return;
        }
        instance = this;
    }

    // Works with ShopScript.cs to select turret from UI
    public void SelectTurretToBuild(TurretBlueprintScript turret)
    {
        turretToBuild = turret;
        
        // If there was a turret node UI showing, hide it and deselect the selected node
        DeselectNode();
    }

    // If there is already a turret on the node, this selects the node and opens turret UI
    public void SelectNode(NodeScript node)
    {
        // If the same node is clicked more than once consecutively, deselect it
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        // Selects the node and deselects turret to build, if one is selected
        selectedNode = node;
        turretToBuild = null;
        nodeUIScript.SetTarget(node);
    }

    // Deselects the node that is selected and hides the turret node UI
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUIScript.Hide();
    }

    // Returns the currently selected turret
    public TurretBlueprintScript GetTurretToBuild()
    {
        return turretToBuild;
    }

}
