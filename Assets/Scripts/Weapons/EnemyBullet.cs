using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    protected Vector3 target;
    private Rigidbody2D rig;
    [SerializeField] private float speed;

    public void setSpeed(float s) { speed = s; }
    public void SetTarget(Vector3 tgt)
    {
        target = tgt;       
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = (target - transform.position).normalized * speed * Time.fixedDeltaTime;
        if(Vector2.Distance(transform.position, target) < 0.1f)
        {
            
            Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerLife>())
        {
            collision.gameObject.GetComponent<PlayerLife>().OnDamage(1);
            Destroy(gameObject);
        }
    }
}
