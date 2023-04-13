using UnityEngine;
using UnityEngine.EventSystems;

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

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManagerScript = BuildManagerScript.instance;
    }
    
    void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if(!buildManagerScript.CanBuild)
        {
            return;
        }
        if(turret != null)
        {
            Debug.Log("Already has a turret built!"); //TODO
            return;
        }

        buildManagerScript.BuildTurretOn(this);

    }

    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(!buildManagerScript.CanBuild)
        {
            return;
        }

        if(buildManagerScript.HasMoney)
        {
            rend.material.color = hoverColor;   
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

        
    }

    void OnMouseExit()
    {
        if(!buildManagerScript.CanBuild)
        {
            return;
        }
        rend.material.color = startColor;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

}
