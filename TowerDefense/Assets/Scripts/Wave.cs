using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    public GameObject enemy;
    public int count;
    public float rate;
    public float scaleFactor;

    public Wave(GameObject enemy, int count, float rate, float scaleFactor)
    {
        this.enemy = enemy;
        this.count = count;
        this.rate = rate;
        this.scaleFactor = scaleFactor;
    }

   
}
