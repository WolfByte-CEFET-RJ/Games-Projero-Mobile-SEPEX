using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGardenStrategy : IAttackStrategy
{
    private float distanceRandom = 20;
    private GameObject[] batatas;
    public override void attack()
    {
        GameObject batataEscolhida = batatas[Random.Range(0, batatas.Length)];

        float posX = Random.Range(transform.position.x - distanceRandom, transform.position.x + distanceRandom);
        float posY = Random.Range(transform.position.y - distanceRandom, transform.position.y + distanceRandom);
        Vector2 posicaoBatata = new Vector2(posX, posY);
        Instantiate(batataEscolhida, posicaoBatata, transform.rotation);
    }

    public override void chargeAttack()
    {
        //Possivel animacao de carga
        attack();
    }

    // Start is called before the first frame update
    void Start()
    {
        batatas = gameObject.GetComponent<FlipAttack>().getBatatas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
