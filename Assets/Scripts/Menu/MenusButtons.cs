using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenusButtons : MonoBehaviour
{
    [HideInInspector] public bool _podeIniciarJogo = true;
    public static MenusButtons main;
    public int primeiraSessao;
    private void Start()
    {
        primeiraSessao = PlayerPrefs.GetInt("PrimeiraSessao");
        if (primeiraSessao != 1)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.SetInt("PrimeiraSessao", 1);
        }
        
    }
    void Awake(){
        main = this;
    }
    void Update()
    {
        /*if(!EventSystem.current.IsPointerOverGameObject())
        {
            // Desconsidera iniciar o jogo se clicar em um botão
            if (_podeIniciarJogo && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
            {
                // Certifica-se de que está no menu antes de chamar a função
                if (EstouMenu())
                {
                    AudioManager.main.PlaySFX(AudioManager.main.menuBtnIn);   //Rodrigo --> chamando a função PlaySFX para tocar o Button In
                    ChamaFase("GameScene");
                }
            }
        }*/
    }

    public void Creditos()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    //Verica se está no menu para poder chamar
    public bool EstouMenu()
    {
        return SceneManager.GetActiveScene().name == "MainMenu";
    }

    // Função de chamar qualquer cena
    public void ChamaFase(string nomeDaCena)
    {
        //AudioManager.main.PlaySFX(btnIn);     Rodrigo
        //yield return new WaitForSeconds(2f);  Rodrigo
        SceneManager.LoadScene(nomeDaCena);
    }
    // Funcao de iniciar o jogo com o tutorial
    public void TutorialBtn()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        SceneManager.LoadScene("GameScene");
    }

    // Função de sair do jogo
    public void SairJogo()
    {
        //AudioManager.main.PlaySFX(btnOut);
        Application.Quit();
    }
}

