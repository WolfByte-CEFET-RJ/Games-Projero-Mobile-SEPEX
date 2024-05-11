using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyFather//Lembrar de mudar a logica para invokeRepeating, usando o radius e o metodo Distance
{
    protected Transform playerPos;

    protected EnemyFollow move;
    protected Animator anim;

    [SerializeField] protected GameObject targetObj;

    [SerializeField] protected GameObject bullet;

    protected bool onAttack;
    protected bool cancelAttack;
    [SerializeField] private float radius;
    void Awake()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<EnemyFollow>();
        if(GameObject.FindGameObjectWithTag("Player"))
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        targetObj.SetActive(false);
        InvokeRepeating("CheckDistance", 0, 0.25f);
    }

    void CheckDistance()
    {
        if(playerPos)
        {
            float dist = Vector2.Distance(transform.position, playerPos.position);
            if (dist <= radius && !onAttack  && !move.GetOnDamage())
            {
                StartCoroutine(Attack());
            }
        }     
    }
    //private void LateUpdate()
    //{
    //    if(onAttack && move.GetOnDamage())
    //    {
    //        targetObj.SetActive(false);
    //        move.Speed = move.getInitialSpeed();
    //        onAttack = false;
    //    }
    //}
    protected virtual IEnumerator Attack()
    {
        onAttack = true;
        cancelAttack = false;
        anim.SetInteger("isAtk", 1);
        Vector3 target = playerPos.position;
        targetObj.transform.position = target;
        targetObj.SetActive(true);
        move.Speed = 0;
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if(move.GetOnDamage())
            {
                //Debug.Log("Tomou dano");
                targetObj.SetActive(false);
                move.Speed = move.getInitialSpeed();
                cancelAttack = true; 
                anim.SetInteger("isAtk", 0);             
            }
        }
        if(!cancelAttack)
        {
            GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
            targetObj.SetActive(false);
            if (bul.GetComponent<EnemyBullet>())
                bul.GetComponent<EnemyBullet>().SetTarget(target);
            else
                Debug.LogError("Referencie corretamente o GameObject bullet!");
            yield return new WaitForSeconds(0.2f);
            move.Speed = move.getInitialSpeed();
            anim.SetInteger("isAtk", 0);
        }
        
           

        onAttack = false;
    }

    void OnDrawGizmosSelected()//Permite visualizar onde o colisor sera criado
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
