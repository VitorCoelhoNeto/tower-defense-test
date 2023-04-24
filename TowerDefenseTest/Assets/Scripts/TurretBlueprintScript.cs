using UnityEngine;

/*
* This script is a class (hence no monobehavior) that will act as the turrets representation with an associated prefab and a cost
* The System.Serializable allows for this selection on the UnityUI between the prefab and the cost
*
* Works in close relationship with the shop script (ShopScript.cs)
*
* Used by GameObjects: None
*/
[System.Serializable]
public class TurretBlueprintScript
{
    
    public GameObject prefab;
    public int cost;

}
