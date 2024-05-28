using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFather : MonoBehaviour//Classe que sera pai de todos os inimigos
{
    [SerializeField] private int damage;
    private bool canDoDamage = true;

    
    // Update is called once per frame
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerLife>() && canDoDamage)
        {
            collision.gameObject.GetComponent<PlayerLife>().OnDamage(damage);
            StartCoroutine(onDamageBoost());
        }
    }
    IEnumerator onDamageBoost()
    {
        AudioManager.main.PlaySFX(AudioManager.main.hitOnPlayer);   //Rodrigo --> chamando a função PlaySFX para tocar o hit no Player
        canDoDamage = false;
        yield return new WaitForSeconds(0.5f);
        canDoDamage = true;
    }

    private void OnDisable()
    {
        canDoDamage = true;
    }
}
