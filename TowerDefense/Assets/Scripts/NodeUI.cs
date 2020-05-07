using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    private Node target;
    public GameObject ui;
    public Text upgradeCost;
    public Text sellValue;
    public Button upgradeButton;
    public Button sellButton;
    //public GameObject camera;

    public void SetTarget(Node _target)
    {
        target = _target;

        //Vector3 dir = camera.transform.position - transform.position;

        //ovo za drugi UI
        //transform.position = target.GetBuildPosition();

        //Debug.Log(dir.magnitude);
        //transform.LookAt(dir.normalized);
        //transform.localScale *= 5;
        //transform.position = ui.transform.position;

        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellValue.text = "$" + target.turretBlueprint.getSellValue();

        //Vector3 dir = camPosition.position - transform.position;
        //transform.LookAt(dir.normalized);

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

    public void Sell()
    {
        target.SellTurret();
        target.isUpgraded = false;
        BuildManager.instance.DeselectNode();

    }
}
