using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Queue<Wave> red = new Queue<Wave>();
    //public ArrayList waves = new ArrayList();

    public Transform spawnPoint;
    public Text waveCountDownText;
    public float timeBetweenWaves = 5.5f;
    public Text alive;
    public Text currentWave;

    private Wave lastWave;
    public static int fullWaves = 0;
    private int waveIndex = 0;
    private float countdown = 2.5f; //vreme cekanja za spawn

    private Wave nextWave;
    private void Awake()
    {
        waveCountDownText.text = Mathf.Round(countdown).ToString();
    }

  
    private void Start()
    {
        currentWave.text = waveIndex.ToString();
        fullWaves = 0;
        for (int i = 0; i < waves.Length; i++)
        {
            red.Enqueue(waves[i]);
        }
    }
    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            alive.text = EnemiesAlive.ToString();
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); //nezvisno od glavnog programa
            countdown = timeBetweenWaves;
            return;
        }


        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        //waveCountDownText.text = Mathf.Round(countdown).ToString();
        waveCountDownText.text = string.Format("{0:00.00}", countdown);
        //alive.text = fullWaves.ToString();
        
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;
        currentWave.text = PlayerStats.rounds.ToString();
        Wave wave = red.Dequeue();
        EnemiesAlive = wave.count;
        red.Enqueue(wave);

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
             
        waveIndex++;

        if(waveIndex % waves.Length == 0)
        {
            fullWaves++;
        }       
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
