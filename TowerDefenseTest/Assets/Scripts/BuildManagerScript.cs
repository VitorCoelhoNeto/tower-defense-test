using UnityEngine;

/*
* This script is treated as a singleton across the app 
* It serves the purpose of managing which turret is currently selected and the act of building a turret
*
* Works in close relationship with the node and shop scripts (NodeScript.cs and ShopScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class BuildManagerScript : MonoBehaviour
{

    // Private variables
    private TurretBlueprintScript turretToBuild;

    // Public variables
    public static BuildManagerScript instance;
    public GameObject buildEffectPrefab;
    public bool CanBuild {  get{  return turretToBuild != null; }  } // Check if there is a turret selected or not
    public bool HasMoney {  get{  return PlayerStatsScript.Money >= turretToBuild.cost; }  } // Check if player has money for the selected turret or not

    // Checks whether the game already has a build manager, if not, create it 
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error - more than one build manager!"); // TODO
            return;
        }
        instance = this;
    }

    // Build the turret on curret selected node (Used by NodeScript.cs)
    public void BuildTurretOn(NodeScript node)
    {
        // Checks whether the player has enough money for the current selected turret
        if(PlayerStatsScript.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money!"); // TODO
            return;
        }

        // Subtract money from player relative to turret cost, build it and play build effect
        PlayerStatsScript.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject buildEffect = (GameObject)Instantiate(buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        Debug.Log("Turret built. Money left: " + PlayerStatsScript.Money); // TODO
    }

    // Works with ShopScript.cs to select turret from UI
    public void SelectTurretToBuild(TurretBlueprintScript turret)
    {
        turretToBuild = turret;
    }

}
