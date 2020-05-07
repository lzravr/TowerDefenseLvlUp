using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

    //razdvajanje movementa u drugu klasu e16

    [Header("Enemy")]
    public float speed = 10f; //pocetna brzina
    public float maxHealth = 100;   //zivot
    public int bounty = 15;     //nagrada po ubijanju neprijatelja
    public GameObject deathEffect;  //efekat smrti neprijatelja
    public float difScale = 0.2f;   //faktor skaliranja tezine

    [Header("Common")]
    public GameObject body;    //model tela neprijatelja
    public Image healthBar;     //healthBar (slika)
    public GameObject healthObject = null;  //healthBar (Canvas)

    private float startSpeed; 
    private float health;
    private Transform target;   //putokaz koji prati
    private int waypointIndex;  //rbr putokaza

    public void SetHealth()
    {
        health = maxHealth * Mathf.Pow(1f + difScale, WaveSpawner.fullWaves);
        //Debug.Log("HP: " + health + "\n" + maxHealth + " + " + WaveSpawner.fullWaves + ", " + Mathf.Pow(1f + difScale, WaveSpawner.fullWaves));
    }

    private void Start()
    {
        waypointIndex = 0;
        SetHealth();
        //health = maxHealth;
        startSpeed = speed;
        target = Waypoints.points[0];
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //deltaTime, da brzina ne zavisi od fps-a

        if(CameraManager.ActiveCamera != null)
            healthObject.transform.LookAt(CameraManager.ActiveCamera.transform.position);
        else
            healthObject.transform.LookAt(CameraManager.CameraRig.transform.position);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            getNextWaypoint();
            body.transform.LookAt(target.position);
        }

        speed = startSpeed;
    }

    private void getNextWaypoint()
    {
        if(waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    private void EndPath()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }

    public void TakeDamage(float dmg)
    {
        if (health - dmg <= 0f)
        {
            Die();
            return;
        }

        health -= dmg;

        healthBar.fillAmount = health / maxHealth; 

    }

    public void ApplyEffect(string type, float amount)
    {
        switch (type)
        {
            case "slow":
                {
                    speed = startSpeed * (1f - amount);
                    break;
                }
            default:
                break;
        }
    }
    

    private  void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
        PlayerStats.money += bounty;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        WaveSpawner.EnemiesAlive--;
    }

  

}
