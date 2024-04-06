using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public string waveName;
    public int numOfEnemies;
    public GameObject[] typeOfEnemies;
    public GameObject boss;
    public float spawnInterval;

    public Wave(int qtdEnemies, string wName, GameObject[] tEn, GameObject b, float spInter)
    {
        numOfEnemies = qtdEnemies;
        waveName = wName;
        typeOfEnemies = tEn;
        boss = b;
        spawnInterval = spInter;
    }
}
public class WaveSpawner : MonoBehaviour
{
    public bool iniciarModoInfinito = false;
    public Wave[] waves;
    public GameObject botao, timer;
    public Timer x;
    public GameObject flip;

    private Wave currentWave;
    private int currentWaveNumber;

    int t = 0;
    private bool canSpawn=true;
    private float nextSpawnTime;

    [SerializeField] private Animator anim;
    private PlayerLife playerLife;
    GameObject[] totalEnemies;
    GameObject[] totalWarnings;

    private int totalInimigos;
    private bool controleNovaWave;
    private void Start()
    {
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }
    private void Update()
    {
        if(!iniciarModoInfinito)
            currentWave = waves[currentWaveNumber];
        else
        {
            if(!controleNovaWave)
            {
                criarWaveInfinita();
                controleNovaWave = true;
            }
        }
        SpawnWave();
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalWarnings = GameObject.FindGameObjectsWithTag("Warning");
        if (totalEnemies.Length == 0 && !canSpawn /*&& currentWaveNumber + 1 != waves.Length*/)
        {
            OnBot();

        }
        if (currentWaveNumber + 1 == waves.Length && currentWave.numOfEnemies == 0 && totalEnemies.Length == 0 && totalWarnings.Length==0 && !iniciarModoInfinito)
        {
            iniciarModoInfinito = true;
            totalInimigos = waves[4].numOfEnemies + 5;
        }
    }
    void criarWaveInfinita()
    {
        Wave novaWave = new Wave(totalInimigos, waves[4].waveName, waves[4].typeOfEnemies, waves[4].boss, waves[4].spawnInterval);
        currentWave = novaWave;
        totalInimigos += 5;
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
    void OffBot()
    {
        botao.SetActive(false);
        anim.Play("ShopExit");
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
        
    }
    public void ClickNext()
    {
        OffBot();      
        TimerOn();

    }
    public void TimerOn()
    {
        timer.SetActive(true);
        botao.SetActive(false);
        x.currentTime = 5.5f;
        x.enabled=true;
        x.timerText.color = Color.yellow;
    }
    public void TimerOff()
    {
        timer.SetActive(false);
        SpawnNextWave();
    }
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime<Time.time)
        {
            t=0;
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];

            Vector2 randomSpawnPoint = new Vector2(Random.Range(-19, 19), Random.Range(-7, 28));
            Instantiate(randomEnemy, randomSpawnPoint, Quaternion.identity);
            GameObject waveBoss = currentWave.boss;
            currentWave.numOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numOfEnemies == 1)
            {
                Instantiate(waveBoss, randomSpawnPoint, Quaternion.identity);
                currentWave.numOfEnemies--;
                

            }
            if(currentWave.numOfEnemies == 0)
            {
                canSpawn = false;             
                if (iniciarModoInfinito)
                    controleNovaWave = false;

            }
        }
        

    }
    
    

}
