using UnityEngine;

/*
* This script is a class (hence no monobehavior) that will act as the turrets representation with an associated prefab and a cost/upgrade cost and upgrade prefab
* The System.Serializable allows for this selection on the UnityUI between the prefabs and the costs
*
* Works in close relationship with the shop and Node scripts (ShopScript.cs, NodeScript.cs)
*
* Used by GameObjects: None
*/
[System.Serializable]
public class TurretBlueprintScript
{
    
    public GameObject prefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradeCost;

    // Returns half the purchasing price
    public int GetSellValue()
    {
        return cost / 2;
    }

}
