using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAttack : MonoBehaviour
{
    private Animator anim;
    public IAttackStrategy attackSelected;//Aqui vao uma das 3 opcoes de ataque: Sonic, Bomba patch e Bears Garden, ambos
    //Implementando essa interface
    

    void Start()
    {
        anim = GetComponent<Animator>();
        choiseAttack();
    }
    

    private void choiseAttack()
    {
        int attackChoise = Random.Range(0, 3);
        switch (attackChoise)
        {
            case 0: attackSelected = gameObject.AddComponent<SanicStrategy>();//Alternativa da unity pra new SanicStrategy().
                break;
            case 1:
                attackSelected = gameObject.AddComponent<BearGardenStrategy>();
                break;
            case 2:
                attackSelected = gameObject.AddComponent<BombaPatchStrategy>();
                break;
        }
        anim.SetInteger("jogoAtaque", attackChoise);
        Debug.Log("Ataque " + attackChoise);
    }
}
