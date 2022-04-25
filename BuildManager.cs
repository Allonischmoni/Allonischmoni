using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    void Awake()
    {
        if (instance!= null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;

    }

    public GameObject standardTurretPrefab;
    public GameObject charmanderTurretPrefab;
    public GameObject meowthTurretPrefab;
    public GameObject arktipTurretPrefab;
    public GameObject nidoranWTurretPrefab;
    

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Nodes selectNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectNode = null;

        DeselectNode();
    }
    public void DeselectNode()
    {
        selectNode = null;
        nodeUI.Hide();
    }
    public void SelectNode(Nodes node)
    {
        if (selectNode == node)
        {
            DeselectNode();
            return;
        }
        selectNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    
    
}
