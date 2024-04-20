using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class IAttackStrategy : MonoBehaviour
{
    public abstract void attack();
    public abstract void chargeAttack();
}
