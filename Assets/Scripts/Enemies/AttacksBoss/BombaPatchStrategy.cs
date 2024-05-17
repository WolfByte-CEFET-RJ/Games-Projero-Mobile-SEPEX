using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPatchStrategy : IAttackStrategy
{
    private GameObject ball;//Aqui temos o prefab da bola
    private GameObject currentBall;//Ja aqui, temos a instancia atual da bola, que sera impulsionada e depois destruida
    private float downgrade = 5;
    private Transform playerPos;
    private FlipCutscene speed;
    public override void attack()
    {
        //Vector3 direction = playerPos.position - transform.position;
        //currentBall.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
        //Destroy(currentBall, 10);

        currentBall.AddComponent<EnemyBullet>();
        currentBall.GetComponent<EnemyBullet>().SetTarget(playerPos.position);
        currentBall.GetComponent<EnemyBullet>().setSpeed(400);
    }

    public override void chargeAttack()
    {
        currentBall = Instantiate(ball, transform.position + (Vector3.down * downgrade), transform.rotation);
        StartCoroutine(controllSpeed());
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = gameObject.GetComponent<FlipCutscene>();
        ball = gameObject.GetComponent<FlipAttack>().getBall();
        playerPos = FindObjectOfType<PlayerMove>().transform;
    }

    IEnumerator controllSpeed()
    {
        speed.stopMovement();
        yield return new WaitForSeconds(1.5f);
        attack();
        speed.startMovement();

    }
}
