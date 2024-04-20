using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPatchStrategy : IAttackStrategy
{
    public override void attack()
    {
        Debug.Log("Atacando como bomba patch");
    }

    public override void chargeAttack()
    {
        Debug.Log("Carregando ataque como bomba patch");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
