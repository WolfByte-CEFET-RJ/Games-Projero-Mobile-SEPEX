using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDash : MonoBehaviour
{
    private SanicStrategy san;
    [SerializeField] private float speed;
    void Start()
    {
        san = gameObject.GetComponentInParent<SanicStrategy>();
        if(!san)
        {
            Debug.LogError("Esse script foi anexado no objeto errado\nEle foi criado apenas para o prefab SpinDash, que" +
                "deve ser instanciado como filho do fliperama");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && san.getOnAtack())
        {
            StartCoroutine(stopBoss());//Parar o boss quando ele encosta no player, pra evitar sair do mapa
        }
    }

    IEnumerator stopBoss()
    {
        yield return new WaitForSeconds(0.125f);
        san.StopBoss();
    }

    private void LateUpdate()
    {
        transform.Rotate(speed * Time.deltaTime * Vector3.forward);
    }
}
