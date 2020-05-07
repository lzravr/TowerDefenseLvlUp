using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    //singlton
    public static BuildManager instance;
    public NodeUI nodeUI;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than 1 BuldManagers");
            return;
        }
        instance = this;
    }
    
    public GameObject buildEffectPrefab;
    public GameObject sellEffectPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    private Turret selectedTurret;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }
    
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void SelectTurretToBuild(TurretBlueprint turr)
    {
        turretToBuild = turr;
         DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        DeselectNode();
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }


    public Node GetSelectedNode()
    {
        return selectedNode;
    }

    public void DeselectNode()
    {   
        if(selectedNode != null)
        {
            Turret t = selectedNode.turret.GetComponent<Turret>();
            if (t != null)
                t.secondaryCamera.enabled = false;

        }
        selectedNode = null;
        nodeUI.Hide();
    }

}
