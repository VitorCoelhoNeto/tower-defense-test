using UnityEngine;
using UnityEngine.UI;

/*
* This script handles the selection of the Turrets Node UI to take actions on already built turrets 
*
* Works in close relationship with the node, and build manager scripts (NodeScript.cs and BuildManagerScript.cs)
*
* Used by GameObjects: NodeUI
*/

public class NodeUIScript : MonoBehaviour
{
    // Public variables
    public GameObject nodeUI;
    public Text upgradeCost;
    public Text sellValue;
    public Button turretUpgradeButton;
    
    // Private variables
    private NodeScript target;

    // Sets the target node (This is called on SelectNode on the buildManagerScript, which is called on a Node which already has a turret built on it)
    public void SetTarget(NodeScript _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        // If the built turret on this Node isn't already upgraded, set the cost of its upgrade on the UI
        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeCost.fontSize = 18;
            turretUpgradeButton.interactable = true;

            sellValue.text = "$" + target.turretBlueprint.GetSellValue();
        }
        else // Else, make the button not interactable and change text to display that the turret is maxed out
        {
            upgradeCost.text = "MAXED";
            upgradeCost.fontSize = 14;
            turretUpgradeButton.interactable = false;

            sellValue.text = "$" + (target.turretBlueprint.GetSellValue() + (target.turretBlueprint.upgradeCost / 2));
        }

        nodeUI.SetActive(true);
    }

    // Hides the turret node UI
    public void Hide()
    {
        nodeUI.SetActive(false);
    }

    // Calls the upgrade function on the target node and deselects it, which in turn hides the turret node UI
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManagerScript.instance.DeselectNode();
    }

    // Sell the turret and deselect the currently selected node
    public void Sell()
    {
        target.SellTurret();
        target.isUpgraded = false;
        BuildManagerScript.instance.DeselectNode();
    }
}
