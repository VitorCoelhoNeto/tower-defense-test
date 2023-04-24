using UnityEngine;

/*
* This script is used to select the intended turret to be built and pass it on to the build manager
*
* Works in close relationship with the build manager script (BuildManagerScript.cs) and the turret class (TurretBlueprintScript.cs)
*
* Used by GameObjects: UI -> Shop; UI -> Shop -> Shop's children
*/

public class ShopScript : MonoBehaviour
{

    // Public variables
    // These two variables are not the prefabs directly because we want a class that allows us to select
    //      the prefab with an associated cost to it, hence, the turret blueprint class
    public TurretBlueprintScript standardTurret;
    public TurretBlueprintScript missileTurret; 

    // Private variables
    BuildManagerScript buildManagerScript;

    // Initiate the buildManagerScript
    void Start()
    {
        buildManagerScript = BuildManagerScript.instance;
    }
    
    // Select the standard turret
    public void SelectStdTurret()
    {
        Debug.Log("Std Turret Selected"); // TODO
        buildManagerScript.SelectTurretToBuild(standardTurret);
    }

    // Select the missile launcher turret
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected"); // TODO
        buildManagerScript.SelectTurretToBuild(missileTurret);
    }

}
