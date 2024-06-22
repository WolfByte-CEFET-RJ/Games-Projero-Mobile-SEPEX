using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadradoTutorial : MonoBehaviour
{
    public bool entrou = false;
    private Animator anim;
    public TutorialManager tutorialManager;
    public Sprite quadVerde;
    private SpriteRenderer spriteRenderer;

    private void Start()//--> inicia o codigo pegando o sprite do GameObject Quadrado
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()//--> verifica se a quantidade de quadrados andados e igual a 4 para assim destruir os quadrados
    {
        if(tutorialManager.quadradosAndados== 4)
        {
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)//--> verifica se o Player ta entrando pela primeira vez no quadrado e aumenta o numero de quadradosAndados
    {
        if (!entrou && other.CompareTag("Player"))
        {
            AudioManager.main.PlaySFX(AudioManager.main.menuBtnIn);
            tutorialManager.IncrementaQuadrado();
            entrou = true;
            MudarSprite();
            
        }
    }
    private void MudarSprite()//--> muda o sprite do quadrado para verde, sinalizando que ele ja entrou nele
    {
        if (spriteRenderer != null && quadVerde != null)
        {
            spriteRenderer.sprite = quadVerde;
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)//--> destroi depois de um delay
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
