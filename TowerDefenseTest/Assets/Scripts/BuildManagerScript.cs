using UnityEngine;

public class BuildManagerScript : MonoBehaviour
{

    // Private variables
    private TurretBlueprintScript turretToBuild;

    // Public variables
    public static BuildManagerScript instance;
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public bool CanBuild {  get{  return turretToBuild != null; }  }

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error - more than one build manager!");
            return;
        }
        instance = this;
    }

    public void BuildTurretOn(NodeScript node)
    {
        if(PlayerStatsScript.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money!"); // TODO
            return;
        }

        PlayerStatsScript.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret built. Money left: " + PlayerStatsScript.Money); // TODO
    }

    public void SelectTurretToBuild(TurretBlueprintScript turret)
    {
        turretToBuild = turret;
    }

}
