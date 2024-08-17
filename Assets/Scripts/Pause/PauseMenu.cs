using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel; //--> colocar o painel com os botões de resumir e de sair pro menu

    public void Pause() 
    {
        if (PlayerScore.Instance != null)
        {
            PlayerScore.Instance.PausarTimer();
        }
        PausePanel.SetActive(true); //--> ativa o painel
        Time.timeScale = 0f; //--> pausa o jogo
    }

    public void Resume()
    {
        if (PlayerScore.Instance != null)
        {
            PlayerScore.Instance.DespausarTimer();
        }
        PausePanel.SetActive(false); //--> desliga o painel
        Time.timeScale = 1f; //--> resume o jogo
    }

    public void Menu(int sceneID)
    {
        Time.timeScale = 1f; //--> volta o tempo do jogo pra 1 (vulgo resumir)
        SceneManager.LoadScene(sceneID); //--> chama a Scene do menu (caso troque na hora da build, tem que trocar o valor tb)
    }
}
