using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaPatchStrategy : IAttackStrategy
{
    private GameObject ball;//Aqui temos o prefab da bola
    private GameObject currentBall;//Ja aqui, temos a instancia atual da bola, que sera impulsionada e depois destruida
    private const float downgrade = 5;
    private Transform playerPos;
    private Transform targetTransf;
    private EnemyFollow enFollow;
    public override void attack()
    {
        //Vector3 direction = playerPos.position - transform.position;
        //currentBall.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
        //Destroy(currentBall, 10);

        AudioManager.main.PlaySFX(AudioManager.main.kickBallSfx);
        SoccerBall soc = currentBall.GetComponent<SoccerBall>();
        soc.SetTarget(targetTransf.position);
        soc.setSpeed(400);
    }

    public override void chargeAttack()
    {
        currentBall = Instantiate(ball, transform.position + (Vector3.down * downgrade), transform.rotation);
        targetTransf = currentBall.GetComponent<SoccerBall>().getTargetTransf();
        targetTransf.localScale = Vector3.one;
        StartCoroutine(controllSpeed());
    }

    // Start is called before the first frame update
    void Start()
    {
        enFollow = gameObject.GetComponent<EnemyFollow>();
        ball = gameObject.GetComponent<FlipAttack>().getBall();
        playerPos = FindObjectOfType<PlayerMove>().transform;
    }

    IEnumerator controllSpeed()
    {
        enFollow.Speed = 0;
        for(int i=0; i<10; i++)
        {
            targetTransf.position = playerPos.position;
            AudioManager.main.PlaySFX(AudioManager.main.aimBallSfx);
            yield return new WaitForSeconds(0.15f);
        }
        targetTransf.position = playerPos.position;
        yield return new WaitForSeconds(0.3f);
        attack();
        enFollow.Speed = enFollow.getInitialSpeed();
        targetTransf.localScale = Vector3.zero;

    }
}
