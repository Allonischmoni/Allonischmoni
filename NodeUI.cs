using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public GameObject upgradeButton;
    public GameObject secondUpgradeButton;

    public Nodes nodes;

    public Text damageText;

    private Nodes target;

    public Text sellAmount;

    void Start()
    {
        ui.SetActive(false);
        secondUpgradeButton.SetActive(false);
    }
    public void SetTarget(Nodes _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (target.isUpgradet == true)
        {
            upgradeButton.SetActive(false);
            Debug.Log("is already upgradet");
            secondUpgradeButton.SetActive(true);
        }else
        {
            upgradeButton.SetActive(true);
            secondUpgradeButton.SetActive(false);
        }
        if (target.isTwoTimesUpgradet == true)
        {
            secondUpgradeButton.SetActive(false);
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        GameObject t = target.GetTurret();
        if(t.tag == "Child")
        {
            Transform child = t.transform.GetChild(0);
            KillCounter damage2 = child .GetComponent<KillCounter>();
            damageText.text = damage2.GetDamage().ToString() + "dmg";
        }else
        {
            KillCounter damage = t.GetComponent<KillCounter>();
            damageText.text = damage.GetDamage().ToString() + "dmg";
        }
        
        
        ui.SetActive(true);
        
    }
    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void SecondUpgrade()
    {
        target.SecondUpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}