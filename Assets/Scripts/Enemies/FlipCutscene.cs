using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCutscene : MonoBehaviour
{
    private CameraFollow cam;
    private EnemyFollow enFollow;
    private bool control;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("CameraPivot").GetComponent<CameraFollow>();
        cam.setPlayerTransf(this.transform);
        enFollow = gameObject.GetComponent<EnemyFollow>();
        if(enFollow == null)
        {
            enFollow = gameObject.GetComponentInChildren<EnemyFollow>();
        }
    }

    public void setPlayerTransform()//Metodos executados no inicio, pra mostrar a cutscene do boss escolhendo o ataque
    {//Vai ser chamado num AnimationEvent no final da animação dele escolhendo ataque
        cam.setPlayerTransf(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void stopMovement()
    {
        enFollow.Speed = 0;
    }
    public void startMovement()
    {
        if (!control)
        {
            enFollow.Speed = enFollow.getInitialSpeed();
            control = true;
        }
        
    }
}
