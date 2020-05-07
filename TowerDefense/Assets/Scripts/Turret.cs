using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private EnemyMovement targetEnemy;
    
    public Camera secondaryCamera;

    [Header("General")]
    public float range = 15f;
    public float turnSpeed = 10;
    public string type;
    public AudioSource audioEffect = null;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int dmgOverTime = 30;
    public string effectType = "none";
    public float effectPower = 0.45f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public AudioSource sound;
    

    [Header("Setup")]
    public Transform partToRotate;
    public string enemyTag = "Enemy";
    public Transform firePoint;
    


	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //sound.pitch = Random.Range(1.9f, 2.7f);
        secondaryCamera.enabled = false;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<EnemyMovement>();
        }
        else
            target = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    impactEffect.Stop();
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                    sound.Stop();
                    //sound.enabled = false;
                }
            }
            return;
        }
            
        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
                if (audioEffect != null)
                    audioEffect.Play();
                
            }

            fireCountdown -= Time.deltaTime;
        }
        
	}

    private void Laser()
    {
        targetEnemy.TakeDamage(dmgOverTime * Time.deltaTime);
        targetEnemy.ApplyEffect(effectType, effectPower);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        sound.Play();

        
        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * transform.localScale.x / 2;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir); 
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>(); //uzimanje skripte bullet

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

}
