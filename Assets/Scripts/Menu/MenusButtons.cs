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
    public GameObject animacaoAtualLobo;
    public GameObject animacaoAtualClique;
    public GameObject animacaoFuturaLobo;

    private void Start()
    {
        primeiraSessao = PlayerPrefs.GetInt("PrimeiraSessao");
        if (primeiraSessao != 1)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.SetInt("PrimeiraSessao", 1);
        }
        animacaoFuturaLobo.SetActive(false);
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
        //SceneManager.LoadScene(nomeDaCena);  Bruno

        StartCoroutine(TransicaoDeCena(nomeDaCena));
        
    }
    private IEnumerator TransicaoDeCena(string nomeDaCena)
    {
        // Desabilitar o GameObjectscom a animação do lobo dormindo
        if (animacaoAtualLobo != null)
        {
            Animator animAtualLobo = animacaoAtualLobo.GetComponent<Animator>();
            if (animAtualLobo != null)
            {
                animAtualLobo.speed = 0f; // Pausa a animação
            }
            Destroy(animacaoAtualLobo); // Agora destroi o GameObject
        }

        // Aumentar a velocidade da animação do CliqueParaComeçar
        if (animacaoAtualClique != null)
        {
            Animator animClique = animacaoAtualClique.GetComponent<Animator>();
            if (animClique != null)
            {
                animClique.speed = 5.0f; // Ajuste a velocidade conforme necessário
            }
        }
        // Ativar a animação do lobo e esperar o seu final para mudar de cena
        if (animacaoFuturaLobo != null)
        {
            animacaoFuturaLobo.SetActive(true); // Certifique-se de ativar o GameObject

            // Verifica se o Animator está presente e obtém a animação
            Animator animFuturaLobo = animacaoFuturaLobo.GetComponent<Animator>();
            if (animFuturaLobo != null)
            {
                // Espera até que a animação termine
                while (!animFuturaLobo.GetCurrentAnimatorStateInfo(0).IsName("Awakening") ||
                       animFuturaLobo.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    yield return null;
                }
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no objeto animacaoFuturaLobo.");
            }
        }
        else
        {
            Debug.LogWarning("animacaoFuturaLobo não está atribuído.");
        }
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

