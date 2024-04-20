using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanicStrategy : IAttackStrategy
{
    public override void attack()
    {
        Debug.Log("Atacando como Sonic");
    }

    public override void chargeAttack()
    {
        Debug.Log("Carregando ataque de Sonic");
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
