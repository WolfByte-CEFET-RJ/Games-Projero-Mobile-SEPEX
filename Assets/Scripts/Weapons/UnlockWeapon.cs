using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour//Joel-> Script obsoleto. Desbloquear armas será algo que ocorrerá na loja apenas. Em breve deleto o script.
{
    [SerializeField] private GameObject weaponToUnlock;
    [SerializeField][Range (1,3)] private int weaponIndex;//Numero entre 1,2 e 3 
    private void Start()
    {
        weaponToUnlock = GameObject.FindGameObjectWithTag("Weapon" + weaponIndex);
        //Weapon[] ws = FindObjectsOfType<Weapon>();
        //foreach (Weapon w in ws)
        //{
        //    if(w.gameObject.tag == "Weapon"+weaponIndex.ToString())
        //    {
        //        weaponToUnlock = w.gameObject;
        //    }
        //}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            weaponToUnlock.GetComponentInChildren<Weapon>().enabled = true;
            weaponToUnlock.GetComponentInChildren<SpriteRenderer>().enabled = true;
            Destroy(gameObject);
        }
    }
}
