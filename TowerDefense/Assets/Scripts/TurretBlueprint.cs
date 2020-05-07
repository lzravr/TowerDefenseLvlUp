using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

    // Use this for initialization
    public GameObject prefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int getSellValue()
    {
        return cost / 2;
    }

}
