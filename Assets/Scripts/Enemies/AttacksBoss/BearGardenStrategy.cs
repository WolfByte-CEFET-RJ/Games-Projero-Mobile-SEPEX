using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGardenStrategy : IAttackStrategy
{
    public override void attack()
    {
        Debug.Log("atacando como bear garden");
    }

    public override void chargeAttack()
    {
        Debug.Log("Carregando ataque de bear garden");
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
