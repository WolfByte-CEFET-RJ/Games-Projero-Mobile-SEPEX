using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rig.velocity == Vector2.zero)
        {
            anim.SetInteger("transition", 0);
        }
        else
        {
            anim.SetInteger("transition", 1);
        }
    }
}
