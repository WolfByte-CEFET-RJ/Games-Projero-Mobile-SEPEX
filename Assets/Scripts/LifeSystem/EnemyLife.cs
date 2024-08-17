using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : LifeSystem
{
    [Header("CommonEnemySettings")]
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject damageText;
    private int verifica;
    void Start()
    {

    }
    public override void OnDamage(int dmg)
    {
        base.OnDamage(dmg);
        if (damageText != null)
        {
            string dano = dmg.ToString();
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", dano);
        }

        if (currentLife <= 0)
        {
            Instantiate(coin, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        verifica = PlayerPrefs.GetInt("Tutorial");
        // Notifica o EnemyManager que este inimigo foi destruído
        if (PlayerScore.Instance != null && verifica != 1)
        {
            PlayerScore.Instance.EnemyDestroyed();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}