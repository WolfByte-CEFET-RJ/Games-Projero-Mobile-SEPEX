using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float Speed;
    private float initialSpeed;
    private Transform Target;
    private bool onDamage;

    public void SetOnDamage(bool b) { onDamage = b; }
    public bool GetOnDamage() { return onDamage; }
    public float getInitialSpeed() { return initialSpeed; }

    void Start()
    {
        initialSpeed = Speed;
        if(GameObject.FindGameObjectWithTag("Player") != null)
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        changeRotation();
        if (Target && !onDamage)
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        else if (Target && onDamage)
            transform.position = Vector2.MoveTowards(transform.position, Target.position, -Speed * 1.2f * Time.deltaTime);
    }

    void changeRotation()
    {
        if(Target.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
