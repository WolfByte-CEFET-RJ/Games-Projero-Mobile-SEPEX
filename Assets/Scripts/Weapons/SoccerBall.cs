using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : EnemyBullet
{
    [SerializeField] private Transform targetTransf;

    public Transform getTargetTransf() { return targetTransf; }

    private void LateUpdate()
    {
        target = targetTransf.position;//Atualizando a cada momento nosso target (ja que a bola nao segue uma posicao fixa)
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        /*
            Aqui, a logica da bola eh basicamente inverter a sua direcao quando bate na parede

            isso porque o alvo que a bola segue eh um objeto filho da propria bola, ou seja, onde a bola vai, o proprio alvo vai tambem. Como
        se fosse, no minecraft, montar no porco com uma cenoura na vara

            Sabendo disso, conseguimos reverter a direcao da bola do seguinte modo:

            Se eh uma parede dos lados, a gente apenas inverte a posicao x do alvo. Ou seja, se o alvo esta a direita da bola, ao tocar numa
        parede lateral, vai se tornar um alvo a esquerda da bola

            Se eh uma parede de cima/baixo, uma logica parecida, mas invertendo a posicao y
        */
        if (collision.CompareTag("ParedeLateral"))
        {
            AudioManager.main.PlaySFX(AudioManager.main.hitBallWallSfx);
            float newPosX = targetTransf.localPosition.x * -1;
            targetTransf.localPosition = new Vector3(newPosX, targetTransf.localPosition.y, targetTransf.localPosition.z);
        }
        if(collision.CompareTag("ParedeVertical"))
        {
            AudioManager.main.PlaySFX(AudioManager.main.hitBallWallSfx);
            float newPosY = targetTransf.localPosition.y * -1;
            targetTransf.localPosition = new Vector3(targetTransf.localPosition.x, newPosY, targetTransf.localPosition.z);
        }

    }
}
