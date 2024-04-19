using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] protected int currentLife;
    protected int maxLife;

    void Awake()
    {
        maxLife = currentLife;
    }


    public virtual void OnDamage(int dmg)
    {
        currentLife -= dmg;
    }
}
