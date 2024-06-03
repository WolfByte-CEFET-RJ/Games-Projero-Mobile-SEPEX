using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCutscene : MonoBehaviour
{
    private CameraFollow cam;
    private EnemyFollow enFollow;
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
        enFollow.Speed = enFollow.getInitialSpeed();
        int gameChosen = GetComponent<Animator>().GetInteger("jogoAtaque");
        AudioManager.main.changeBgm(AudioManager.main.flipGamesMusic[gameChosen]);
    }

    public void ChangeBgmOnDeathCutscene(AudioClip music)//Esses 2 ultimos metodos tambem sao de AnimationEvent. Serao na animacao Death
    {
        AudioManager.main.changeBgm(music);
    }
    public void DestroyFlip(AudioClip music)
    {
        GetComponent<FlipAttack>().SetCanAttack(false);
        AudioManager.main.changeBgm(music);
        Destroy(gameObject);
    }
}
