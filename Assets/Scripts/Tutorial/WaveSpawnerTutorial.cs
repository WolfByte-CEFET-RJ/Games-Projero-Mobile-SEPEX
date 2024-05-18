using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerTutorial : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public bool iniciarTutorial = false;
    public Wave[] waves;
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    int t = 0;
    public GameObject botao;
    GameObject[] totalEnemies;
    GameObject[] totalWarnings;
    [SerializeField] private Animator anim;

    private PlayerLife playerLife;
    private void Start()
    {
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }
    private void Update()
    {
        if (iniciarTutorial)
        {
            SpawnWave();
        }
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalWarnings = GameObject.FindGameObjectsWithTag("Warning");
        if (totalWarnings.Length == 0 && totalEnemies.Length == 0 && !canSpawn )
        {
            OnBot();
        }
    }
    public void PodeSpawnarTutorial()
    {
        iniciarTutorial = true;
    }
    void OnBot()
    {
        if (t == 0)
        {
            
            botao.SetActive(true);
            playerLife.ResetLife();//Recuperar vida automaticamente
            anim.Play("ShopEnter");
            t++;
        }

    }
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            t = 0;
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];

            Vector2 randomSpawnPoint = new Vector2(Random.Range(-19, 19), Random.Range(-7, 28));
            Instantiate(randomEnemy, randomSpawnPoint, Quaternion.identity);
            GameObject waveBoss = currentWave.boss;
            currentWave.numOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numOfEnemies == 1 && waveBoss != null)
            {
                Instantiate(waveBoss, randomSpawnPoint, Quaternion.identity);
                currentWave.numOfEnemies--;


            }
            if (currentWave.numOfEnemies == 0)
            {
                canSpawn = false;
                

            }
        }


    }
}
