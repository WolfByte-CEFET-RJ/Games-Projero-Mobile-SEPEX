using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeroBearGarden : EnemyRanged
{
    // Start is called before the first frame update
    void Start()
    {
        if(!anim)
            anim = GetComponentInChildren<Animator>();
        anim.SetInteger("trans", 1);
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
        for (int i = 0; i < 8; i++)
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
        if (!cancelAttack && bullet)
        {
            GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
            
            if (bul.GetComponent<EnemyBullet>())
                bul.GetComponent<EnemyBullet>().SetTarget(target);
            else
                Debug.LogError("Referencie corretamente o GameObject bullet!");
            
            
            //anim.SetInteger("isAtk", 0);
        }

        targetObj.SetActive(false);
        move.Speed = move.getInitialSpeed();
        onAttack = false;
        yield return new WaitForSeconds(0.2f);
    }
}
