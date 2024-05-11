using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeroBearGarden : EnemyRanged
{
    [SerializeField] private bool isRanged;
    // Start is called before the first frame update
    void Start()
    {
        if(!anim)
            anim = GetComponentInChildren<Animator>();
        anim.SetInteger("trans", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRanged)
        {
            //Logica do ataque das tropas ranged (arqueiro e mago)
        }
        else
        {
            //Logica das tropas melee (guerreiro e vilao)
        }
    }

    protected override IEnumerator Attack()
    {
        onAttack = true;
        cancelAttack = false;
        anim.SetTrigger("atk");//É apenas por essa linha que tive que sobrescrever o metodo para as tropas ranged. (Quase)Todo o resto irá funcionar igualmente
        Vector3 target = playerPos.position;
        targetObj.transform.position = target;
        targetObj.SetActive(true);
        move.Speed = 0;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if (move.GetOnDamage())
            {
                targetObj.SetActive(false);
                move.Speed = move.getInitialSpeed();
                cancelAttack = true;
                //anim.SetInteger("isAtk", 0);
            }
        }
        if (!cancelAttack)
        {
            GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
            targetObj.SetActive(false);
            if (bul.GetComponent<EnemyBullet>())
                bul.GetComponent<EnemyBullet>().SetTarget(target);
            else
                Debug.LogError("Referencie corretamente o GameObject bullet!");
            yield return new WaitForSeconds(0.2f);
            move.Speed = move.getInitialSpeed();
            //anim.SetInteger("isAtk", 0);
        }



        onAttack = false;
    }
}
