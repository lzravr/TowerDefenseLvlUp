using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color noMoneyColor;
    public Color selectColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    private Renderer rend;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(turret != null)
        {
            buildManager.SelectNode(this);
            //rend.material.color = selectColor;
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
        {
            Debug.Log("No money");
            return;
        }

        PlayerStats.money -= blueprint.cost;
        GameObject turretTmp = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = turretTmp;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("No money");
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        turret.GetComponent<Turret>().secondaryCamera.enabled = false;
        Destroy(turret);

        GameObject turretTmp = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = turretTmp;
        //turret.GetComponent<Turret>().secondaryCamera.enabled = true;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);

        isUpgraded = true;
        Destroy(effect, 5f);
    }

    public void SellTurret()
    {
        PlayerStats.money += turretBlueprint.getSellValue();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
    }


    private void OnMouseEnter()
    {
        //no hover over shop
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = noMoneyColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
