using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBlueprint standardTurret;
    public TurretBlueprint charmanderTurret;
    public TurretBlueprint meowthTurret;
    public TurretBlueprint arktipTurret;
    public TurretBlueprint nidoranWTurret;
    public TurretBlueprint abraTurret;
    public TurretBlueprint drapfelTurret;
    public TurretBlueprint pidgeyTurret;
    public TurretBlueprint phanpyTurret;
    public TurretBlueprint karpadorTurret;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectCharmanderTurret()
    {
        buildManager.SelectTurretToBuild(charmanderTurret);
    }
    public void SelectMeowthTurret()
    {
        buildManager.SelectTurretToBuild(meowthTurret);
    }
    public void SelectArktipTurret()
    {
        buildManager.SelectTurretToBuild(arktipTurret);
    }
    public void SelectNidoranWTurret()
    {
        buildManager.SelectTurretToBuild(nidoranWTurret);
    }
    public void SelectAbraTurret()
    {
        buildManager.SelectTurretToBuild(abraTurret);
    }
    public void SelectDrapfelTurret()
    {
        buildManager.SelectTurretToBuild(drapfelTurret);
    }
    public void SelectPidgeyTurretToBuild()
    {
        buildManager.SelectTurretToBuild(pidgeyTurret);
    }
    public void SelectPhanpyTurretToBuild()
    {
        buildManager.SelectTurretToBuild(phanpyTurret);
    }
    public void SelectKarpadorTurretToBuild()
    {
        buildManager.SelectTurretToBuild(karpadorTurret);
    }
}
