using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public string nodeType;

    public Vector3 positionOffset;
    
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgradet = false;
    public bool isTwoTimesUpgradet = false;
    public bool waterNode = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager; 

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        rend.enabled = false;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());
    }
    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        if (waterNode)
        {
            if (!blueprint .water)
            {
                Debug.Log("Not able to swim!");
                return;
            }
        }
        if (!waterNode)
        {
            if (!blueprint.land)
            {
                Debug.Log("Not able to stand!");
                return;
            }
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        if (turretBlueprint.hasUpgrade != true)
        {
            return;
        }
        if (isUpgradet != false)
        {
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        Debug.Log("Turret upgradet!");
        
        
        isUpgradet = true;


    }
    public void SecondUpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.secondUpgradeCost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        if (turretBlueprint.hasSecondUpgrade != true)
        {
            return;
        }
        if (isTwoTimesUpgradet != false)
        {
            return;
        }
        PlayerStats.Money -= turretBlueprint.secondUpgradeCost;

        Destroy(turret);
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.secondUpgradedPrefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);

        isTwoTimesUpgradet = true;

        Debug.Log("Turret upgradet!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        isUpgradet = false;
        isTwoTimesUpgradet = false;
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        rend.enabled = true;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
        
    }
    public GameObject GetTurret()
    {
        return turret;
    }

    void OnMouseExit()
    {
        rend.enabled = false;
        rend.material.color = startColor;
    }
}
