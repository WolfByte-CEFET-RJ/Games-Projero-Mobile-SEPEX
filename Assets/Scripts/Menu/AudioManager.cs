using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Rodrigo --> bibliotecas a mais
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource soundFX;

    [Header("Audio Clips")]
    [Header("\tGame, Menu and other musics")]
    public AudioClip musicGame;
    public AudioClip musicMenu;
    public AudioClip flipMusic;
    public AudioClip[] flipGamesMusic;/*Quando a musica do Bomba Patch ficar pronta, adiciona-la ao elemento 2 desse array*/

    [Header("\tHit effects")]
    public AudioClip hitOnPlayer;
    public AudioClip hitOnEnemy;

    [Header("\tMenu effects")]
    public AudioClip menuBtnIn;
    public AudioClip menuBtnOut;

    [Header("\tShop effects")]
    public AudioClip[] upgradesSfx;
    public AudioClip errorSfx;
    public AudioClip lockOpen;
    public AudioClip lockClose;

    [Header("\tTimer effects")]
    public AudioClip timerSfx;
    public AudioClip timerFinalSfx;

    [Header("\tBear Garden Effects")]
    public AudioClip spawnSfx;

    [Header("\tBomba Patch effects")]
    public AudioClip aimBallSfx;
    public AudioClip kickBallSfx;
    public AudioClip hitBallWallSfx;

    [Header("Tutorial")]
    public AudioClip typeSound;
    [Header("Audio Mixer")]
    [SerializeField] AudioMixer myMixer;

    public static AudioManager main;    //Rodrigo --> função contendo o script AudioManager que pode ser acessada por outros scripts

    void Awake() {
        main = this;    //Rodrigo --> atribuindo esse script à variável main para chamar as funções em outros scripts
    }

    void Start() {  //Rodrigo --> função Start que checa em qual cena o programa está e toca a música correspondente
        int estado = ChecaCena();
        switch (estado) {   //Rodrigo --> estado = 1 --> cena GameScene .. estado = 2 --> cena MainMenu
            case 1:
                backgroundMusic.clip = musicGame;
                backgroundMusic.Play();
                break;
            case 2:
                backgroundMusic.clip = musicMenu;
                backgroundMusic.Play();
                break;
            default:
                backgroundMusic.Stop();
                break;
        }

        if(!PlayerPrefs.HasKey("musicVolume")){
            myMixer.SetFloat("musica", 0);
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else Load(1);

        if(!PlayerPrefs.HasKey("sfxVolume")){
            myMixer.SetFloat("sfx", 0);
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }
        else Load(2);
    }

    int ChecaCena() {
        if(SceneManager.GetActiveScene().name == "GameScene")
            return 1;
        else if(SceneManager.GetActiveScene().name == "MainMenu")
            return 2;
        else
            return 3;
    }
    public void changeBgm(AudioClip aud) //Funcao para mudar a musica de fundo após o metodo Start
    {
        backgroundMusic.clip = aud;
        backgroundMusic.Play();
    }
    public void PlaySFX(AudioClip _clip) {  //Rodrigo --> função que recebe uma das variáveis com clips de áudio e toca uma vez (SFX)
        soundFX.PlayOneShot(_clip);
    }
    public void StopAudio()
    {
        soundFX.Stop();
    }
    void Load(int _estado) //Rodrigo --> função para inserir os valores previamente salvos pelo jogador
    {
        switch(_estado){    //_estado = 1 (música)  _estado = 2 (sfx)
            case 1:
                ChangeVolume(1, PlayerPrefs.GetFloat("musicVolume"));
                break;
            case 2:
                ChangeVolume(2, PlayerPrefs.GetFloat("sfxVolume"));
                break;
            default:
                break;
        }
    }

    public void ChangeVolume(int _estado, float _volume)   //Rodrigo --> função que controla os sliders recebendo _estado para decidir qual volume é modificado (para manter tudo em um código)
    {   //Rodrigo --> _estado = 1 --> mudança na música ... _estado = 2 --> mudança no sfx
        switch (_estado) {
            case 1:
                //Rodrigo --> modifica o valor (AudioMixer funciona em log, por isso a função Mathf)
                if(_volume > 0) myMixer.SetFloat("musica", Mathf.Log10(_volume)*20);
                else myMixer.SetFloat("musica", -80);   //Rodrigo --> setando o valor -80dB pois 0dB é o valor máximo
                
                PlayerPrefs.SetFloat("musicVolume", _volume);    //Rodrigo --> guardando o valor em PlayerPrefs
                break;
            case 2:
                //Rodrigo --> modifica o valor (AudioMixer funciona em log, por isso a função Mathf)
                if(_volume > 0) myMixer.SetFloat("sfx", Mathf.Log10(_volume)*20);
                else myMixer.SetFloat("sfx", -80);   //Rodrigo --> setando o valor -80dB pois 0dB é o valor máximo
                
                PlayerPrefs.SetFloat("sfxVolume", _volume);    //Rodrigo --> guardando o valor em PlayerPrefs
                break;
            default:
                break;
        }

    }
}
