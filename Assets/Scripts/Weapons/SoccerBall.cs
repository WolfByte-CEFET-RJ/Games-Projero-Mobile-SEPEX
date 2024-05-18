using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : EnemyBullet
{
    [SerializeField] private Transform targetTransf;

    public Transform getTargetTransf() { return targetTransf; }

    private void LateUpdate()
    {
        target = targetTransf.position;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)//Me lembrem de documentar bonitinho dps
    {//Ate pq agora nao ta 100% funcional. Vou ver ainda como posso corrigir
        base.OnTriggerEnter2D(collision);//O que posso falar agora e que abaixo seria a logica do quique da bola na parede

        if(collision.CompareTag("ParedeLateral"))
        {
            if (transform.rotation.y == 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(collision.CompareTag("ParedeVertical"))
        {
            if (transform.rotation.z == 0)
                transform.eulerAngles = new Vector3(0, 0, 180);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }
}
