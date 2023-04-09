using UnityEngine;

public class ShopScript : MonoBehaviour
{

    // Public variables
    public TurretBlueprintScript standardTurret;
    public TurretBlueprintScript missileTurret; 

    // Private variables
    BuildManagerScript buildManagerScript;

    void Start()
    {
        buildManagerScript = BuildManagerScript.instance;
    }
    
    public void SelectStdTurret()
    {
        Debug.Log("Std Turret Selected");
        buildManagerScript.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManagerScript.SelectTurretToBuild(missileTurret);
    }

}
