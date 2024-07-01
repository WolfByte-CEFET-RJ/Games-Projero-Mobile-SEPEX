using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_Life : LifeSystem
{
    [Header("FlipSettings")]
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject healthObj;
    private HealthBarFlip health;

    private bool onDeath;
    // Start is called before the first frame update
    public void Start()
    {
        AudioManager.main.changeBgm(AudioManager.main.flipMusic);
        health = healthObj.GetComponent<HealthBarFlip>();
        health.ActiveBar();
        health.SetMaxHealth(maxLife);
    }
    public override void OnDamage(int dmg)
    {
        base.OnDamage(dmg);
        health.SetHealth(currentLife);
        if (damageText != null)
        {
            string dano = dmg.ToString();
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", dano);
        }
        if (currentLife <= 0 && !onDeath)
        {
            //AudioManager.main.changeBgm(AudioManager.main.musicGame);
            //Destroy(gameObject);
            GetComponent<Animator>().SetTrigger("death");//Chama animacao de morte, que vai fazer a troca de sprites e troca de musicas
            GetComponent<FlipAttack>().setAttackSelectedToNull();
            GetComponent<FlipCutscene>().stopMovement();
            onDeath = true;
        }

    }

}
