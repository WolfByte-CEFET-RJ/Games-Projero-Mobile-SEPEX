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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(tutorialManager.quadradosAndados== 4)
        {
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!entrou && other.CompareTag("Player"))
        {
            tutorialManager.IncrementaQuadrado();
            entrou = true;
            MudarSprite();
            
        }
    }
    private void MudarSprite()
    {
        if (spriteRenderer != null && quadVerde != null)
        {
            spriteRenderer.sprite = quadVerde;
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
