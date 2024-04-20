using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAttack : MonoBehaviour
{
    
    public IAttackStrategy attackSelected;//Aqui vao uma das 3 opcoes de ataque: Sonic, Bomba patch e Bears Garden, ambos
    //Implementando essa interface
    

    void Start()
    {
       
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
        Debug.Log("Ataque " + attackChoise);
    }
}
