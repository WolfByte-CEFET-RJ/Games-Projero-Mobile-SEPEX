using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex=0;
    public int quadradosAndados;
    public WaveSpawner waveSpawnerTutorial;
    public Wave tutorialWave;
    private bool canSpawn=false;
    private float nextSpawnTime;
    private bool entrou = false;
    public TextPopUp popUpTexto;
    private PlayerLife playerLife;
    private PlayerCoin playerCoin;
    public GameObject confirmarPanel;
    public GameObject resumeBtn, menuBtn;
    public TextMeshProUGUI textoConfimar;
    public TextMeshProUGUI textoBtnJogar;
    private int verifica;


    private void Start()
    {
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();//--> pega o componente de vida do Player
        playerCoin = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCoin>();//--> pega o componente de moedas do Player
        entrou = false;//--> inicia entrou como falso
        verifica = PlayerPrefs.GetInt("Tutorial");//-->verifica vai ser igual ao valor de Tutorial
        if (verifica != 1)//--> se verifica for igual a 1 tera o tutorial, caso contrario ele pula.
        {
            PularTutorial();
        }
    }
    private void Update()
    {
        SpawnWave();//--> Spawna a wave de tutorial, somente se puder, dentro dele verifica se a wave ja acabou mudando o index apos 7 segundos
        for (int i = 0; i < popUps.Length; i++)//--> Mostra somente o popUp corrrespondente ao index atual
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        
        if (popUpIndex == 0){
            if(quadradosAndados== 4)
            {
                popUpIndex++;//--> Apos andar para os quatro quadrados, aumenta o index, mostrando o proximo popUp

            }
        }
        if (popUpIndex == 1)
        {
            if(entrou == false)//--> verifica se ja permitiu o spawn alguma vez, se nao ele entra no if e permite com um delay de 5 segundos, e logo em seguida muda a variavel entrou para sinalizar que ja permitiu o spawn.
            {
                StartCoroutine(CanSpawnDelay(5f));
                entrou = true;
            }
            
        }
        if (popUpIndex == 2)
        {
            if(popUpTexto.AcabouTexto())//-->verifica se o texto ja acabou de digitar
            {
                ConfirmarPulo();
            }
        }
    }
    public void IncrementaQuadrado()//-->nome ja diz
    {
        quadradosAndados++;
    }
    /*private IEnumerator PodeSpawnComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        waveSpawnerTutorial.PodeSpawnarTutorial();
    }*/
    private IEnumerator PopUpIndexAddDelay(float delay)//--> aumeta o index do popUp com delay
    {
        yield return new WaitForSeconds(delay);
        popUpIndex++;
    }
    private IEnumerator CanSpawnDelay(float delay)//--> permite o spawn com delay
    {
        yield return new WaitForSeconds(delay);
        canSpawn=true;
    }
    void SpawnWave()//--> funcao igual a do WaveSpawner
    {

        if (canSpawn && nextSpawnTime < Time.time) { 
            GameObject randomEnemy = tutorialWave.typeOfEnemies[Random.Range(0, tutorialWave.typeOfEnemies.Length)];

            Vector2 randomSpawnPoint = new Vector2(Random.Range(-19, 19), Random.Range(-7, 28));
            Instantiate(randomEnemy, randomSpawnPoint, Quaternion.identity);
            GameObject waveBoss = tutorialWave.boss;
            tutorialWave.numOfEnemies--;
            nextSpawnTime = Time.time + tutorialWave.spawnInterval;
            if (tutorialWave.numOfEnemies == 1 && waveBoss != null)
            {
                Instantiate(waveBoss, randomSpawnPoint, Quaternion.identity);
            tutorialWave.numOfEnemies--;


            }
            if (tutorialWave.numOfEnemies == 0)
            {
                canSpawn = false;
                StartCoroutine(PopUpIndexAddDelay(7f));

            }
        }



    }
    public void ConfirmarPulo()//-->  abre o painel de confirmacao de pular o tutorial, ou caso tenha acabado o tutorial.
    {
        popUps[popUpIndex].SetActive(false);
        confirmarPanel.SetActive(true);//--> liga o painel
        Time.timeScale = 0f; //--> pausa o jogo
        if (popUpTexto.AcabouTexto())//--> verifica se a parte de confirmar foi acionada porque o tutorial acabou
        {
            resumeBtn.SetActive(false);
            menuBtn.SetActive(true);
            textoConfimar.text = "O Tutorial Acabou!";
            textoBtnJogar.text = "Iniciar Jogo";

        }
        else
        {
            resumeBtn.SetActive(true);
            menuBtn.SetActive(false);
            textoConfimar.text = "Tem Certeza Que Deseja Pular O Tutorial?";
            textoBtnJogar.text = "Sim";
        }               
    }
    public void NaoPular()//--> nao pula o tutorial
    {
        confirmarPanel.SetActive(false); //--> desliga o painel
        Time.timeScale = 1f; //--> resume o jogo
    }
    public void VoltarMenu()//--> volta pro menu
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PularTutorial()//--> Inicia o jogo
    {
        confirmarPanel.SetActive(false); //--> desliga o painel
        Time.timeScale = 1f; //--> resume o jogo
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//--> procura na cena objetos com a tag "Enemy"
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);//--> destroi cada objeto na cena  com a tag "Enemy"
        }
        GameObject[] warnings = GameObject.FindGameObjectsWithTag("Warning");//--> procura na cena objetos com a tag "Warning"
        foreach (GameObject warning in warnings)
        {
            Destroy(warning);//--> destroi cada objeto na cena  com a tag "Warning"
        }
        waveSpawnerTutorial.PodeSpawnarTutorial(); //--> permite que a wave seja spawnada de forma normal
        playerLife.ResetLife(); //--> Reseta a vida do Player
        playerCoin.CoinReset(); //--> Reseta as moedas do Player
        PlayerPrefs.SetInt("Tutorial", 0);
        Destroy(gameObject); //--> destroi todos os elementos do tutorial

    }
}
