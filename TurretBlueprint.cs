using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public bool hasUpgrade;
    public bool hasSecondUpgrade;

    public bool land = true ;
    public bool water;

    public GameObject secondUpgradedPrefab;
    public int secondUpgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
