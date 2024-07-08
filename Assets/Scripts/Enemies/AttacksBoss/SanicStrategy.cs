using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanicStrategy : IAttackStrategy
{
    private GameObject spinDash;
    private GameObject currentSpinDash;
    private EnemyFollow enFollow;

    private bool onAtack;
    public bool getOnAtack() { return onAtack; }

    private const float boostSpeed = 3.125f;
    public override void attack()
    {
        StartCoroutine(attackLogic());
    }

    public override void chargeAttack()
    {
        enFollow.Speed = 0;
        
        attack();
    }

    // Start is called before the first frame update
    void Start()
    {
        spinDash = gameObject.GetComponent<FlipAttack>().getSpinDash();
        currentSpinDash = Instantiate(spinDash, transform.localPosition, transform.rotation, gameObject.transform);
        currentSpinDash.SetActive(false);

        enFollow = gameObject.GetComponent<EnemyFollow>();
    }
    IEnumerator attackLogic()
    {
        yield return new WaitForSeconds(0.5f);
        currentSpinDash.SetActive(true);
        yield return new WaitForSeconds(1);
        enFollow.Speed = enFollow.getInitialSpeed() * boostSpeed;
        onAtack = true;
        yield return new WaitForSeconds(1.5f);
        StopBoss();//Parar o boss quando nao encosta no player apos um tempo
    }
    
    public void StopBoss()//Metodo pra evitar que o player saia do limite do mapa (senao, ele pode ser forcado pelo ataque ser muito forte)
    {
        currentSpinDash.SetActive(false);
        enFollow.Speed = enFollow.getInitialSpeed();
        onAtack = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        StopBoss();//Parar o boss quando ele encosta no player, pra evitar sair do mapa
    //    }
    //}
}
